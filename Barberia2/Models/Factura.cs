using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    internal class Factura
    {
        public int idfactura { get; set; }
        public int cliente_idcliente { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        public int barberosidbarberos { get; set; }
    }
}
