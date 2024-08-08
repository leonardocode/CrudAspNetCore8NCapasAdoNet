using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo celular es obligatorio")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El campo email es obligatorio")]
        public string Email { get; set; }

        public bool Estado { get; set; }
        public string? usuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? usuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
