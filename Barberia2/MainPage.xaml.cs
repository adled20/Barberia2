using System.Threading.Tasks;

namespace Barberia2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private async void Ingreso(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Seleccion());
        }
        private async void Navegacion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Crear_Usuario());
        }
    }

}
