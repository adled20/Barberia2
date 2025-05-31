using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testagenda
{
    public class mostar_barbero
    {
        [Key]
        public int idbarberos { get; set; }
        public string nombre { get; set; }

        public string fotobarbero { get; set; }

        public string? apodo { get; set; }
        
    }
}
