using Barberia2.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json;

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
                foreach (var item in response.Where(p => p.estado?.ToLower() == "activo"))
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
            nombreLabel.Text = productoSeleccionado.nombre;
            precioLabel.Text = $"${productoSeleccionado.precio}";
            descripcionLabel.Text = productoSeleccionado.descripcion ?? "No añadido";

            // Opcional: Deseleccionar visualmente el item
            productosCollectionView.SelectedItem = null;
        }
    }
    private async void AñadirCarrito(object sender, EventArgs e)
    {
      
        try
        {
            var boton = sender as Button;
            int id = Convert.ToInt32(boton?.CommandParameter);
           
            var client = new HttpClient();
            var json = await client.GetStringAsync($"https://barberiaapi.onrender.com/api/productos/{id}");
            var producto = JsonSerializer.Deserialize<Productos>(json);
            
            if (producto != null)
            {
                var ResultadoInsert = await App.dataBase.InsertProducto(new Model.carrito
                {
                    Nombre = producto.nombre,
                    Precio = Convert.ToInt32(producto.precio)
                });
                if (ResultadoInsert > 0)
                {
                    await DisplayAlert("Producto Añadido",
                          $"Nombre: {producto.nombre}\nPrecio: ${producto.precio}",
                          "OK");
                }
               

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
      
    }
    private async void VerCarritoCon(object sender, EventArgs e)
    {
        var ListTodos = await App.dataBase.GetAll();
        await Navigation.PushAsync(new VerCarrito(ListTodos));
    }

 }