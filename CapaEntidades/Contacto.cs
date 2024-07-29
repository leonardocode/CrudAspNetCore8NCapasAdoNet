using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
        public string usuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string usuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
