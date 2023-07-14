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
    public partial class CarElectronicF07 : ContentPage
    {
        private string url = "http://192.168.56.1:83/wsPrueba/api/General";
        public CarElectronicF07()
        {
            InitializeComponent();
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                DateTime selecttedDate = FecIngreso.Date;
                string formattedDate = selecttedDate.ToString("yyyyMMdd");

                WebClient orden = new WebClient();
                var mdlTLRM05 = new System.Collections.Specialized.NameValueCollection

                {

                    {"idOrden", "0" },
                    //{"idvehiculo", txtIdVehiculo.Text },
                    {"idusuario", txtIdUsuario.Text },
                    //{"estatus", txtEstatus.Text },
                    {"fecha", formattedDate},
                    {"hora", txtHora.Text },
                    {"kilometraje", txtKilometraje.Text },
                    {"porcentajecomb", txtPorcentajeComb.Text },
                    //{"codtiposervicio", txtCodTipServicio.Text },
                    {"observacion", txtObservacion.Text },
                    {"recomendacion", txtRecomendacion.Text },
                    {"celulartercero", txtCelular.Text },
                    //{"fecha_cierre", formattedDate},

                };

                orden.Headers[HttpRequestHeader.ContentType] = "application/json";
                //armar nuestro json propioro es decir define la tabla y envia el modelo 
                var data = new
                {
                    iStrIdProceso = "orden",
                    iOrden = ToDictionary(mdlTLRM05)
                };

                string json = JsonConvert.SerializeObject(data);
                byte[] byTLRM05 = Encoding.UTF8.GetBytes(json);

                byte[] response = orden.UploadData(url, "POST", byTLRM05);

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

        private void btnConsultar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CarElectronicF08());
        }
    }
}