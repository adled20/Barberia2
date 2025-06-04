using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    public class Combo
    {
        public int idcombo { get; set; }
        public string tipoCombo { get; set; }
        public int costo { get; set; }
        public string descripcion { get; set; }
        public string duracion { get; set; }  // Usa TimeSpan si prefieres manejarlo como duración real
        public string estado { get; set; }
        public string nombre { get; set; }
    }
}
