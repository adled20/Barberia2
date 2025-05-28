using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    public class Productos
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
        public int Stock_minimo { get; set; }
        public string Estado { get; set; }
        public string Imagen { get; set; }
    }
}
