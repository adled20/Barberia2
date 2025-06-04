using Barberia2.Models;
using System.Text;
using System.Text.Json;

namespace Barberia2.Barberos_registros;

public partial class Barbero_reg : ContentPage
{
   
    public Barbero_reg()
	{
		InitializeComponent();
        string nombreBarbero = Preferences.Get("NombreBar", "Barbero");
        NombreBarbero.Text = $"Bienvenido, {nombreBarbero}";
        // Asignar el BindingContext
        BindingContext = this;
    }

    private async void BuscarCliente(object sender, EventArgs e)
    {


        string identificacion = Identificacion.Text.Trim();

        if (string.IsNullOrEmpty(identificacion))
        {
            await DisplayAlert("Error", "Ingrese una identificación", "OK");
            return;
        }

        try
        {
            // 1. Obtener todos los clientes
            using var client = new HttpClient();
            var response = await client.GetAsync("https://barberiaapi.onrender.com/api/cliente");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var clientes = JsonSerializer.Deserialize<List<Cliente>>(json);

                // 2. Buscar por identificación localmente
                var cliente = clientes?.FirstOrDefault(c =>
                    c.identificacion.Equals(identificacion, StringComparison.OrdinalIgnoreCase));

                if (cliente != null)
                {
                    // 3. Rellenar los Entry
                    Nombre.Text = cliente.primernombre;
                    Nombre2.Text = cliente.segundoNombre;
                    Apellido.Text = cliente.primerapellido;
                    Apellido2.Text = cliente.segundoApellido;
                    Telefono.Text = cliente.telefono;
                }
                else
                {
                    await DisplayAlert("No encontrado", "Cliente no registrado, Favor de registrarlo", "OK");
                    Nombre.Text = "";
                    Nombre2.Text = "";
                    Apellido.Text = "";
                    Apellido2.Text = "";
                    Telefono.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void AñadirCliente(object sender, EventArgs e)
    {
        using var vericlient = new HttpClient();
        var veriresponse = await vericlient.GetAsync("https://barberiaapi.onrender.com/api/cliente");

        if (veriresponse.IsSuccessStatusCode)
        {
            var json = await veriresponse.Content.ReadAsStringAsync();
            var clientes = JsonSerializer.Deserialize<List<Cliente>>(json);

            // 2. Buscar por identificación localmente
            var vericliente = clientes?.FirstOrDefault(c =>
                c.identificacion.Equals(Identificacion.Text, StringComparison.OrdinalIgnoreCase));

               if (vericliente!=null)
            {
                await DisplayAlert("ERROR", "Cliente ya registrado", "OK");
                return;

            }
        }
            try
        {
            // 1. Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(Nombre.Text) ||
                string.IsNullOrWhiteSpace(Apellido.Text) ||
                string.IsNullOrWhiteSpace(Identificacion.Text))
            {
                await DisplayAlert("Error", "Complete los campos obligatorios Nombre, Apellido e Identificación", "OK");
                return;
            }


            // 3. Crear objeto cliente
            var nuevoCliente = new
            {
                primernombre = Nombre.Text,
                segundoNombre = Nombre2.Text,
                primerapellido = Apellido.Text,
                segundoApellido = Apellido2.Text,
                telefono = Telefono.Text,
                identificacion = Identificacion.Text
            };

            // 4. Serializar a JSON
            var json = JsonSerializer.Serialize(nuevoCliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // 5. Enviar POST
            var client = new HttpClient();
            var response = await client.PostAsync(
                "https://barberiaapi.onrender.com/api/cliente",
                content);
          
            // 6. Verificar respuesta
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Cliente registrado correctamente", "OK");

                // Limpiar formulario
                Nombre.Text = "";
                Nombre2.Text = "";
                Apellido.Text = "";
                Apellido2.Text = "";
                Telefono.Text = "";
                Identificacion.Text = "";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await DisplayAlert("Error", $"No se pudo registrar: {error}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un problema: {ex.Message}", "OK");
        }
       
    }
    private async void TipoPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        string tipoSeleccionado = (string)picker.SelectedItem;

        if (string.IsNullOrEmpty(tipoSeleccionado))
            return;

        ServicioPicker.IsEnabled = false;
        ServicioPicker.Items.Clear();

        try
        {
           

            // Determinar la URL según la selección
            string url = tipoSeleccionado == "Corte"
                ? "https://barberiaapi.onrender.com/api/cortes"
                : "https://barberiaapi.onrender.com/api/comboes";

            // Hacer la petición
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var servicios = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);

                // Llenar el segundo Picker
                foreach (var servicio in servicios)
                {
                    if (servicio.TryGetValue("nombre", out var nombre))
                        ServicioPicker.Items.Add(nombre.ToString());
                }

                ServicioPicker.IsEnabled = true;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar: {ex.Message}", "OK");
        }
    }
}
