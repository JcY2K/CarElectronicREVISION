using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarElectronic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarElectronicF02 : ContentPage
    {
        public CarElectronicF02(string gUsuario)
        {
            InitializeComponent();
            lblFechaApp.Text= App.GetFechaHoraAcceso();
            lblUsuario.Text = gUsuario;
        }

        private void btnNuevaOrd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CarElectronicF03());
        }

        private void btnMntoOrd_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Alerta", "mantenimiento nueva", "Cerrar");
        }

        private void btnMntoCliente_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CarElectronicF05());
        }

        private void btnMtoVehi_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CarElectronicF06());
        }

        private void btnOrden_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CarElectronicF07());

        }
    }
}