using Barberia2.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Barberia2;

public partial class Combo : ContentPage
{


    public Combo()
    {
        InitializeComponent();
        CargarCombos();
    }

    private async Task<List<ComboCorteDisponible>> ObtenerComboCortes()
    {
        using HttpClient cliente = new HttpClient();
        var respuesta = await cliente.GetStringAsync("https://barberiaapi.onrender.com/api/cortes/Combo_corte_disponible");
        var lista = JsonConvert.DeserializeObject<List<ComboCorteDisponible>>(respuesta);
        return lista;
    }

    private async void CargarCombos()
    {
        var datos = await ObtenerComboCortes();

        var combosAgrupados = datos
            .GroupBy(c => c.idcombo)
            .Select(g =>
            {
                var primero = g.First();
                return new ComboConCortes
                {
                    IdCombo = primero.idcombo,
                    NombreCombo = primero.nombre_combo,
                    TipoCombo = primero.tipoCombo,
                    Duracion = primero.duracion,
                    Costo = primero.costo,
                    Cortes = g.ToList()
                };
            }).ToList();

        ComboCollectionView.ItemsSource = combosAgrupados;
    }

    private async void OnCorteImageClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton boton && boton.BindingContext is ComboCorteDisponible corte)
        {
            // Busca el combo completo al que pertenece este corte
            var combos = ComboCollectionView.ItemsSource as List<ComboConCortes>;
            var combo = combos?.FirstOrDefault(c => c.IdCombo == corte.idcombo);

            if (combo != null)
            {
                await Navigation.PushAsync(new detalle_combo(combo));
            }
        }
    }

    public class ComboConCortes
    {
        public int IdCombo { get; set; }
        public string NombreCombo { get; set; }
        public string TipoCombo { get; set; }
        public string Duracion { get; set; }
        public float Costo { get; set; }
        public List<ComboCorteDisponible> Cortes { get; set; }
    }
}

