using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    internal class Usuarios
    {
       public int idusuarios { get; set; }
        public string email { get; set; }
        public string contraseña { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int cliente_idcliente { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string direccion {  get; set; }
        public string estado { get; set; }
    }
}
