using System.Text.Json;
using Barberia2.Models;

namespace Barberia2.Barberos_registros;

public partial class Descansos_prog : ContentPage
{
	public Descansos_prog()
	{
		InitializeComponent();
        CargarCitasActivas();

    }

    public async Task<List<Agenda>> ObtenerCitasDelBarbero()
    {
        try
        {
            // 1. Obtener el ID del barbero de Preferences
            var barberoIdStr = Convert.ToInt32(Preferences.Get("user_id", "0"));
           

            // 2. Consumir API completa
            using var client = new HttpClient();
            string url = "https://barberiaapi.onrender.com/api/agenda";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Error", "Error al obtener citas", "OK");
                return new List<Agenda>();
            }

            // 3. Deserializar todas las citas
            var json = await response.Content.ReadAsStringAsync();
            var todasLasCitas = JsonSerializer.Deserialize<List<Agenda>>(json) ?? new List<Agenda>();

            // 4. Filtrar localmente por barberoid
            return todasLasCitas.Where(c => c.barberoid == barberoIdStr && c.estado?.Equals("Activo", StringComparison.OrdinalIgnoreCase) == true).ToList();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Excepción: {ex.Message}", "OK");
         return new List<Agenda>();
        }
    }

    private async void CargarCitasActivas()
    {
       

        var citasActivas = await ObtenerCitasDelBarbero();

        if (citasActivas.Any())
        {
           

            // Opcional: Ordenar por fecha de inicio
            citasActivas = citasActivas
                .OrderBy(c => DateTime.Parse(c.tiempo_inicio))
                .ToList();

            citasListView.ItemsSource = citasActivas;
        }
        else
        {
            await DisplayAlert("Info", "No hay citas activas", "OK");
        }

    }
}