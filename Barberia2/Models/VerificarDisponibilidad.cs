using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    class VerificarDisponibilidad
    {
        public int idbarberos { get; set; }
        public TimeOnly tiempo_inicio { get; set; }
        public TimeOnly tiempo_final { get; set; }
        public DateTime dia { get; set; }
    }


}
