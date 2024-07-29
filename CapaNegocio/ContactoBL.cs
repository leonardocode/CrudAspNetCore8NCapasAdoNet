using CapaDatos;
using CapaEntidades;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ContactoBL
    {
        private readonly ContactoDAL contactoDAL;
        public ContactoBL(ContactoDAL contactoDAL)
        {
            this.contactoDAL = contactoDAL;
        }

        public List<Contacto> Lista()
        {
            DataTable dataTable = contactoDAL.Lista();
            List<Contacto> listaContactos = new List<Contacto>();
            try
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Contacto contacto = new Contacto
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Nombre = row["Nombre"].ToString(),
                        Celular = row["Celular"].ToString(),
                        Email = row["Email"].ToString(),
                        Estado = Convert.ToBoolean(row["Estado"])
                    };
                    listaContactos.Add(contacto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaContactos;
        }
    }
}
