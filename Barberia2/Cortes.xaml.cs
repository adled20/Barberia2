using System.Collections.ObjectModel;
using System.Text.Json;
using Barberia2.Models;

namespace Barberia2;

public partial class Cortes : ContentPage
{
    public ObservableCollection<Cortes> ListaCortes { get; } = new();
    public Cortes()
	{
		InitializeComponent();
        BindingContext = this;
        CargarDatos();
    }
    private async void CargarDatos()
    {
        try
        {
            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetStringAsync("https://barberiaapi.onrender.com/api/cortes");

            var cortes = JsonSerializer.Deserialize<List<Cortes>>(respuesta);

            ListaCortes.Clear();
            foreach (var corte in cortes)
            {
                ListaCortes.Add(corte);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private async void DetalleCorte(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var corteSeleccionado = ((ImageButton)sender).CommandParameter;


        // 2. Crear la página destino
        var paginaDestino = new DetalleCorte();

        // 3. Pasar TODOS los datos como BindingContext
        paginaDestino.BindingContext = corteSeleccionado;

        // 4. Navegar
        await Navigation.PushAsync(paginaDestino);
    }
}

