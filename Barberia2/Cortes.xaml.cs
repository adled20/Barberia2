namespace Barberia2;

public partial class Cortes : ContentPage
{
	public Cortes()
	{
		InitializeComponent();

        var cortesEjemplo = new[]
    {
            new
            {
                Id = 1,
                Nombre = "Corte Clásico",
                Descripcion = "Estilo tradicional con tijera y máquina",
                Imagen = "corte_clasico.png",
                Precio = 25.99
            },
            new
            {
                Id = 2,
                Nombre = "Fade Moderno",
                Descripcion = "Degradado suave de largo a corto",
                Imagen = "fade_moderno.png",
                Precio = 30.50
            },
            new
            {
                Id = 3,
                Nombre = "Undercut",
                Descripcion = "Lados cortos con volumen arriba",
                Imagen = "undercut.png",
                Precio = 28.75
            }
        };

        // Asignar como BindingContext
        BindingContext = cortesEjemplo;
    }

    private async void DetalleCorte(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var corteSeleccionado = ((ImageButton)sender).CommandParameter;


        // 2. Crear la página destino
        var paginaDestino = new DetalleCorte();

        // 3. Pasar TODOS los datos como BindingContext
        paginaDestino.BindingContext = corteSeleccionado;

        // 4. Navegar
        await Navigation.PushAsync(paginaDestino);
    }
}

