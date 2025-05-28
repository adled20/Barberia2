using Barberia2.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace Barberia2;

public partial class Products : ContentPage
{
    public ObservableCollection<Productos> Items { get; } = new();

    public Products()
    {
        InitializeComponent();
        BindingContext = this;
        LoadProducts();
    }

    private async void LoadProducts()
    {
        try
        {
            var client = new HttpClient();
            var response = await client.GetFromJsonAsync<List<Productos>>("https://barberiaapi.onrender.com/api/productos");

            if (response != null)
            {
                Items.Clear();
                foreach (var item in response.Where(p => p.Estado?.ToLower() == "activo"))
                {
                    Items.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los productos: {ex.Message}", "OK");
        }
    }

    private void OnProductoSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Productos productoSeleccionado)
        {
            // Actualizar los Labels con los datos del producto seleccionado
            nombreLabel.Text = productoSeleccionado.Nombre;
            precioLabel.Text = $"${productoSeleccionado.Precio}";
            descripcionLabel.Text = productoSeleccionado.Descripcion ?? "No añadido";

            // Opcional: Deseleccionar visualmente el item
            productosCollectionView.SelectedItem = null;
        }
    }
}