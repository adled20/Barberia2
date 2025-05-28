using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia2.Models
{
    public class Usuario
    {
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public int ClienteId { get; set; }
        public string Contrasenia { get; set; }
        public string Estado { get; set; } = "Activo";

        public Usuario()
        {
            FechaRegistro = DateTime.Now;
            Estado = "Activo";
        }
    }
}