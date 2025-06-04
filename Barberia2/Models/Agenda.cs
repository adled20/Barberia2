using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    public class Agenda
    {
        public string tiempo_inicio { get; set; }
        public string tiempo_final { get; set; }
        public string estado { get; set; }
        public int id { get; set; }
        public int usuarios_idusuarios { get; set; }
        public int barberoid { get; set; }
        public int plan_idPlan { get; set; }
        public int corte_idcorte { get; set; }
        public int combo_idcombo { get; set; }
    }
}
