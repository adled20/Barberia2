using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testagenda
{
    public class disponibilidad_agenda
    {
        [Key]
        public int idbarberos { get; set; }
        public TimeOnly? inicio_cita { get; set; } 
        //se multiplica las horas con los minutos para obtener la posicion e

        public TimeOnly? final_cita { get; set; } 

        public TimeOnly hora_entrada { get; set; }
        public TimeOnly hora_salida { get; set; }
    }
}
