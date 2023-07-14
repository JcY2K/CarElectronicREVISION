using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarElectronic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarElectronicF06 : ContentPage
    {
        private string url = "http://192.168.56.1:83/wsPrueba/api/TLRM04";
        public CarElectronicF06()
        {
            InitializeComponent();
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                DateTime selecttedDate = FecCompra.Date;
                string formattedDate = selecttedDate.ToString("yyyyMMdd");

                WebClient vehiculo = new WebClient();
                var mdlTLRM04 = new System.Collections.Specialized.NameValueCollection

                {
                    { "idVehiculo", "0" },
                    {"idcliente",txtIdCliente.Text },
                    { "codMarca", txtCodMarca.Text },
                    { "codModelo", txtCodModelo.Text },
                    { "ANIOFABRICA", txtAñoFabrica.Text },
                    { "paisOrigen", txtPaisOrigen.Text },
                    { "cilindraje", txtCilindraje.Text },
                    { "color", txtColor.Text },
                    { "CODTIPOCOMBUSTIBLE", txtTipoCombustible.Text },
                    { "snAsegurado", txtSnAsegurado.Text },
                    { "fechaCompra", formattedDate },
                    { "placa", txtPlaca.Text },

                };

                vehiculo.Headers[HttpRequestHeader.ContentType] = "application/json";
                //armar nuestro json propioro es decir define la tabla y envia el modelo 
                var data = new
                {
                    iStrIdProceso = "vehiculo",
                    IVehiculo = ToDictionary(mdlTLRM04)
                };

                string json = JsonConvert.SerializeObject(data);
                byte[] byTLRM04 = Encoding.UTF8.GetBytes(json);

                byte[] response = vehiculo.UploadData(url, "POST", byTLRM04);

                //recibe la respuesta del web service 
                string responseString = Encoding.UTF8.GetString(response);

                //Navigation.PushAsync(new MainPage());
                DisplayAlert("Alerta", "Ingreso :" + responseString, "Cerrar");


            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "Cerrar");
            }
        }
        private static Dictionary<string, string> ToDictionary(NameValueCollection collection)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var key in collection.AllKeys)
            {
                var value = collection[key];
                dictionary.Add(key, value);
            }

            return dictionary;
        }

        private void txtCodModelo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodModelo.Text.Trim()))
            {
                DisplayAlert("Alerta", "Debe ingresar un Modelo", "Cerrar");
            }

        }
        //aaaa
        private void txtCodMarca_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodMarca.Text.Trim()))
            {
                DisplayAlert("Alerta", "Debe ingresar una Marca", "Cerrar");
            }
        }
    }
}