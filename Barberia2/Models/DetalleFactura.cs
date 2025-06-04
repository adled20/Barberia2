using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
   public class DetalleFactura
    {
        public int iddetalle_factura { get; set; }
        public int factura_idfactura { get; set; }
        public int cantidad { get; set; }
        public string estado { get; set; }
        public int? corte_idcorte { get; set; }
        public int? combo_idcombo { get; set; }
        public string plan_idPlan { get; set; }
    }
}
