using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    public class Productos
    {
        public int idProducto {get;set;}
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string marca { get; set; }
        public int precio { get; set; }
        public int stock { get; set; }
        public int stock_minimo { get; set; }
        public string estado { get; set; }
        public string imagen { get; set; }
    }
}
