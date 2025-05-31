using System.Text.Json;
using Barberia2.Models;

namespace Barberia2;

public partial class DetalleCorte : ContentPage
{
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
                duracionLabel.Text = "Duracion: "+corte.duracion;
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
        await Navigation.PushAsync(new agenda());
    }
}