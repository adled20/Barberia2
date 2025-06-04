using System.Text.Json;
using Barberia2.Models;

namespace Barberia2;

public partial class DetalleCorte : ContentPage
{
    private int duracionEnMinutos;
    private int idCorte;
    private int usuarioId;
    public DetalleCorte(int idCorte)
	{
		InitializeComponent();
        this.idCorte = idCorte;
        usuarioId = int.Parse(Preferences.Get("user_id", "0"));

        if (usuarioId == 0)
        {
            DisplayAlert("Error", "No se ha encontrado el ID del usuario", "OK");
        }
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
                descripcionLabel.Text = "Descripcion: " + corte.descripcion;

                TimeSpan duracionSpan = TimeSpan.Parse(corte.duracion);
                duracionEnMinutos = (int)duracionSpan.TotalMinutes;
                duracionLabel.Text = $"Duración: {duracionEnMinutos} min";

                tipoLabel.Text = "Tipo: " + corte.tipo;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void Agendar_cita(object sender, EventArgs e)
    {
        // Pasamos duración, comboId = 0, corteId real, planId = 0, usuarioId real
        await Navigation.PushAsync(new agenda(
            duracionCorte: duracionEnMinutos,
            comboId: 0,
            corteId: idCorte,
            planId: 0,
            usuarioId: usuarioId
        ));
    }
}