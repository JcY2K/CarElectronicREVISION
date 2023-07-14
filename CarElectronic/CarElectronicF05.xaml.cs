using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarElectronic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CarElectronicF05 : ContentPage
    {
        //private string url = "http://192.168.56.1:83/wsPrueba/api/General";
        private string url = "http://192.168.56.1:83/wsPrueba/api/General";
        public CarElectronicF05()
        {
            InitializeComponent();
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = dtpFecNac.Date;
                string formattedDate = selectedDate.ToString("yyyyMMdd");

                WebClient cliente = new WebClient();
                var mdlTLRM03 = new System.Collections.Specialized.NameValueCollection
                {
                    { "idCliente", "0" },
                    { "tipoidCliente", txtTipoCl.Text },
                    { "identificacion", txtIdentificacion.Text },
                    { "primer_Nombre", txtPrimNombre.Text },
                    { "segundo_Nombre", txtSegNombre.Text },
                    { "primer_Apellido", txtPrimApellido.Text },
                    { "segundo_Apellido", txtSegApellido.Text },
                    { "nombreCorto", txtNomCorto.Text },
                    { "correo", txtCorreo.Text },
                    { "celular", txtCelular.Text },
                    { "convencional", txtConvencional.Text },
                    { "fechaNacimiento", formattedDate}
                };

                cliente.Headers[HttpRequestHeader.ContentType] = "application/json";
                //armar nuestro json propioro es decir define la tabla y envia el modelo 
                var data = new
                {
                    iStrIdProceso = "cliente",
                    iCliente = ToDictionary(mdlTLRM03)
                };

                string json = JsonConvert.SerializeObject(data);
                byte[] byTLRM03 = Encoding.UTF8.GetBytes(json);

                byte[] response = cliente.UploadData(url, "POST", byTLRM03);
                
                //recibe la respuesta del web service 
                string responseString = Encoding.UTF8.GetString(response);
                if (responseString =="true")
                //Navigation.PushAsync(new MainPage());
                    DisplayAlert("Alerta", "Ingreso :" + responseString, "Cerrar");
                else
                {
                    DisplayAlert("Alerta", "ERROR:" + responseString, "Cerrar");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "Cerrar");
            }
        }
        //
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

        private void txtPrimApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrimApellido.Text.Trim()))
            {
                DisplayAlert("Alerta", "Debe ingresar Primer Apellido/Primer Nombre", "Cerrar");
            }
            else
            {
                if (string.IsNullOrEmpty(txtPrimNombre.Text.Trim()))
                {
                    DisplayAlert("Alerta", "Debe ingresar Primer Nombre", "Cerrar");                    
                }
                else {
                    txtNomCorto.Text = txtPrimNombre.Text.Trim() + " " + txtPrimApellido.Text.Trim();
                }
            }
        }

        private void txtPrimNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrimNombre.Text.Trim()))
            {
                DisplayAlert("Alerta", "Debe ingresar Primer Nombre", "Cerrar");
            }
        }
    }
}