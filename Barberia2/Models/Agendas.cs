using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    public class Agendas
    {
        public int id { get; set; }
        public TimeOnly tiempo_inicio { get; set; }
        public TimeOnly tiempo_final { get; set; }
        public int usuarios_idusuarios { get; set; }

        public int Plan_idPlan { get; set; }

        public int corte_idcorte { get; set; }

        public int combo_idcombo { get; set; }
        public DateTime dia { get; set; }
        
    }
}
