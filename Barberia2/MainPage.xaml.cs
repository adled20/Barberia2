using System.Text.Json;
using System.Threading.Tasks;
using Barberia2.Barberos_registros;
using Barberia2.Models;
using Microsoft.Maui.ApplicationModel.Communication;

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
            string email = Correo.Text;
           string contraseña = Contraseña.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contraseña))
            {
                await DisplayAlert("Error", "Por favor ingrese email y contraseña", "OK");
                return;
            }

            using var client = new HttpClient();

            // 1. Obtener todos los usuarios (o los que coincidan con el email)
            string url = $"https://barberiaapi.onrender.com/api/usuarios_app";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var usuarios = JsonSerializer.Deserialize<List<Usuarios>>(json);

                // 2. Filtrar y validar manualmente
                var usuarioValido = usuarios?.FirstOrDefault(u =>
                    u.email?.Trim().Equals(email, StringComparison.OrdinalIgnoreCase) == true &&
                    u.contraseña == contraseña);

                if (usuarioValido != null)
                {
                    // Guardar datos de usuario si es necesario
                    Preferences.Set("user_id", usuarioValido.idusuarios.ToString());
                    Preferences.Set("user_email", usuarioValido.email);

                    await DisplayAlert("Éxito", "Inicio de sesión correcto", "OK");
                    await Navigation.PushAsync(new Seleccion());
                }
                else
                {
                    string urlbarberos = $"https://barberiaapi.onrender.com/api/barberos";
                    var responsebarberos = await client.GetAsync(urlbarberos);
                    if (responsebarberos.IsSuccessStatusCode)
                    {
                        var jsonBarbero = await responsebarberos.Content.ReadAsStringAsync();
                        var barbero = JsonSerializer.Deserialize<List<Barberos>>(jsonBarbero);
                        var BarberoValido = barbero?.FirstOrDefault(u =>
                  u.identificacion?.Trim().Equals(email, StringComparison.OrdinalIgnoreCase) == true &&
                  u.telefono == contraseña);

                        if (BarberoValido != null)
                        {
                            Preferences.Set("user_id", BarberoValido.idbarberos.ToString());
                            Preferences.Set("user_email", BarberoValido.identificacion);
                            Preferences.Set("NombreBar", BarberoValido.primerNombre);
                            await DisplayAlert("Exito", "Inicio de sesión", "OK");
                            await Navigation.PushAsync(new BarberosSelector());
                        }
                    }
                    await DisplayAlert("Error", "Credenciales incorrectas", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Error al conectar con el servidor", "OK");
            }
        }
    








        private async void Navegacion(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Crear_Usuario());
        }
    }

}
