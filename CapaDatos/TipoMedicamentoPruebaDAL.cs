using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class TipoMedicamentoPruebaDAL
    {
        public List<TipoMedicamentoPrueba> Lista()
        {
            List<TipoMedicamentoPrueba> lista = new List<TipoMedicamentoPrueba>();
            lista.Add(new TipoMedicamentoPrueba()
            {
                Id = 1,
                Nombre = "nombre 1",
                Descripcion = "descripcion 1"
            });

            lista.Add(new TipoMedicamentoPrueba()
            {
                Id = 2,
                Nombre = "nombre 2",
                Descripcion = "descripcion 2"
            });
            return lista;
        }
    }
}
