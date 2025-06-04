using System.Threading.Tasks;

namespace Barberia2;

public partial class BarberosSelector : ContentPage
{
	public BarberosSelector()
	{
		InitializeComponent();
	}

    private async void Registrosbtn(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Barberos_registros.Barbero_reg());
    }
    
    private void Historicobtn(object sender, EventArgs e)
    {

    }
}