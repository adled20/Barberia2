using Barberia2.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Barberia2;

public partial class Planes : ContentPage
{
    
    public Planes()
	{

		InitializeComponent();
        CargarPlanes();
    }

    private async void CargarPlanes()
    {
        HttpClient client = new HttpClient();
        var response = await client.GetStringAsync("https://barberiaapi.onrender.com/api/plan");
        var planesData = JsonConvert.DeserializeObject<List<plan>>(response);

        List<PlanConCorte> planesCompletos = new List<PlanConCorte>();

        foreach (var p in planesData)
        {
            // Obtener los datos del corte por API según el ID
            var corteResponse = await client.GetStringAsync($"https://barberiaapi.onrender.com/api/cortes/{p.corte_idcorte}");
            var corte = JsonConvert.DeserializeObject<Peina>(corteResponse);

            // Combinar todo
            PlanConCorte planConCorte = new PlanConCorte
            {
                idPlan = p.idPlan,
                nombre = p.nombre,
                descripcion = p.descripcion,
                fecha_inicio = p.fecha_inicio,
                fecha_fin = p.fecha_fin,
                tipo = p.tipo,
                descuento = p.descuento,
                corte_idcorte = p.corte_idcorte,
                costo = p.costo,
                corte = corte
            };

            planesCompletos.Add(planConCorte);
        }

        planList.ItemsSource = planesCompletos;
    }
    private async void VerDetalle_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedPlan = button.BindingContext as PlanConCorte;
        await Navigation.PushAsync(new detalle_plan(selectedPlan));
    }
}
