using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    public class PlanConCorte
    {
        public int idPlan { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public string tipo { get; set; }
        public float descuento { get; set; }
        public int corte_idcorte { get; set; }
        public float costo { get; set; }
        public Peina corte { get; set; }
    }
}
