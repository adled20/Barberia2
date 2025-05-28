namespace Barberia2;

public partial class Seleccion : ContentPage
{
	public Seleccion()
	{
		InitializeComponent();
	}

    private async void  Cortes(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new Cortes());
    }
    private async void Productos(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Productos());
    }
}