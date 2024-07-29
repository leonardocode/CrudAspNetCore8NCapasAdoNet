using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class TipoMedicamentoBL
    {
        private readonly TipoMedicamentoPruebaDAL tipoMedicamentoPruebaDAL = new TipoMedicamentoPruebaDAL();
        public List<TipoMedicamentoPrueba> Lista()
        {
            return tipoMedicamentoPruebaDAL.Lista();
        }
    }
}
