using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    public class ComboCorteDisponible
    {
        public int idcombo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string duracion { get; set; }  
        public string tipo { get; set; }
        public float costo { get; set; }
        public string imagen { get; set; }
        public string nombre_combo { get; set; }
        public string tipoCombo { get; set; }
    }
}
