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
        await Navigation.PushAsync(new agenda(duracionEnMinutos));
    }
}