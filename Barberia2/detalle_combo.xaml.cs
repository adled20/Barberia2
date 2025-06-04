using Barberia2.Models; // Asegúrate de tener el namespace correcto para Agendas
using Microsoft.Maui.Storage; // Para usar Preferences

namespace Barberia2;

public partial class detalle_combo : ContentPage
{
    private Combo.ComboConCortes combo;
    private int duracionEnMinutos;

    public detalle_combo(Combo.ComboConCortes comboSeleccionado)
    {
        InitializeComponent();
        combo = comboSeleccionado;
        MostrarDetalle();
    }

    private void MostrarDetalle()
    {
        nombreLabel.Text = combo.NombreCombo;
        precioLabel.Text = $"Valor: ${combo.Costo}";

        TimeSpan duracionSpan = TimeSpan.Parse(combo.Duracion);
        duracionEnMinutos = (int)duracionSpan.TotalMinutes;
        duracionLabel.Text = $"Duración: {duracionEnMinutos} min";

        tipoLabel.Text = "Tipo: " + combo.TipoCombo;
        CortesCollectionView.ItemsSource = combo.Cortes;
    }

    private async void Agendar_cita(object sender, EventArgs e)
    {
        // Obtener el ID del usuario guardado en Preferences
        int usuarioId = int.TryParse(Preferences.Get("user_id", "0"), out int idUsuario) ? idUsuario : 0;

        if (usuarioId == 0)
        {
            await DisplayAlert("Error", "No se encontró el usuario. Inicia sesión nuevamente.", "OK");
            return;
        }

        await Navigation.PushAsync(new agenda(
            duracionCorte: duracionEnMinutos,
            comboId: combo.IdCombo,
            corteId: 0, 
            planId: 0,  
            usuarioId: usuarioId
        ));
    }
}
