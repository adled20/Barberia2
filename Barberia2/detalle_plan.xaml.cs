using Barberia2.Models;

namespace Barberia2;

public partial class detalle_plan : ContentPage
{
    PlanConCorte plan; 

    public detalle_plan(PlanConCorte planSeleccionado)
	{
		InitializeComponent();
        plan = planSeleccionado;
        MostrarDetalle();
    }
    private void MostrarDetalle()
    {
        // Mostrar imagen del corte (si tienes la URL o el nombre de archivo)
        imgCorte.Source = plan.corte.imagen; // Asegúrate que sea una URL válida o un recurso local

        // Datos del corte asociado al plan
        lblNombreCorte.Text = plan.corte.nombre;
        lblTipoCorte.Text = plan.corte.tipo;
        lblDescripcion.Text = plan.descripcion;


        lblDuracion.Text = $"{plan.descuento}% de descuento";
        lblInicio.Text = plan.fecha_inicio;
        lblFin.Text = plan.fecha_fin;
    }
}