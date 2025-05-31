using System.Collections.ObjectModel;
using Barberia2.Model;


namespace Barberia2;

public partial class VerCarrito : ContentPage
{
    carrito ub = null;
    public ObservableCollection<carrito> Listproductos { get; set; }
    
    public VerCarrito(List<carrito> carritos)
	{
		InitializeComponent();
		Listproductos = new ObservableCollection<carrito>(carritos);
		this.BindingContext = this;
        llenaTotal();

    }
    private async void Eliminar(object sender, EventArgs e)
    {

        var boton = sender as Button;
        int id = Convert.ToInt32(boton?.CommandParameter);
        bool veri = await DisplayAlert("Verificacón", "¿Seguro que desea eliminar este producto?", "si", "no");
        if (veri != false)
        {
            ub = await App.dataBase.GetOne(id);
            await App.dataBase.Delete(ub);
            var itemToRemove = Listproductos.FirstOrDefault(u => u.Id == id);
            Listproductos.Remove(itemToRemove);
            llenaTotal();
        }



    }
    private async void llenaTotal()
    {
        int total = await App.dataBase.SumarPrecio();
        Total.Text = $"Total: {total}";
    }
}