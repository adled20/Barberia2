using System.Text.Json;
using Barberia2.Models;

namespace Barberia2;

public partial class DetalleCorte : ContentPage
{
    private int duracionEnMinutos;
    public DetalleCorte(int idCorte)
	{
		InitializeComponent();
        CargarDetalle(idCorte);
    }
    private async void CargarDetalle(int id)
    {
        try
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync($"https://barberiaapi.onrender.com/api/cortes/{id}");
            var corte = JsonSerializer.Deserialize<Peina>(json);

            if (corte != null)
            {
                nombreLabel.Text = corte.nombre;
                precioLabel.Text = $"Valor: ${corte.costo}";
                imagenCorte.Source = corte.imagen;
                descripcionLabel.Text = "Descripcion: "+corte.descripcion;

                TimeSpan duracionSpan = TimeSpan.Parse(corte.duracion);
                duracionEnMinutos = (int)duracionSpan.TotalMinutes;
                duracionLabel.Text = $"Duración: {duracionEnMinutos} min";

               tipoLabel.Text = "Tipo: "+corte.tipo;


               
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

      

    }

   private async void Agendar_cita(object sender, EventArgs e)
{
        await Navigation.PushAsync(new agenda(duracionEnMinutos));
    }
}