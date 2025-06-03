using System.Collections.ObjectModel;
using System.Text.Json;
using Barberia2.Models;

namespace Barberia2;

public partial class Cortes : ContentPage
{
    public ObservableCollection<Peina> ListaCortes { get; } = new();

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
            var client = new HttpClient();
            var json = await client.GetStringAsync("https://barberiaapi.onrender.com/api/cortes");

            var cortes = JsonSerializer.Deserialize<List<Peina>>(json);

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
    private async  void DetalleCorte(object sender, EventArgs e)
    {
        var boton = (ImageButton)sender;  // o Button, depende del control

        
        if (int.TryParse(boton.CommandParameter?.ToString(), out int idcorte))
        {
            // Navega a la página detalle pasando el id
            await Navigation.PushAsync(new DetalleCorte(idcorte));
        }
        else
        {
            await DisplayAlert("Error", "ID inválido o no asignado", "OK");
        }

    }

}


