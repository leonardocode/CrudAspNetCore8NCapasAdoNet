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
        private readonly ContactoDAL _contactoDAL;
        private readonly ILogger<ContactoBL> _logger;

        public ContactoBL(ContactoDAL contactoDAL, ILogger<ContactoBL> logger)
        {
            this._contactoDAL = contactoDAL;
            _logger = logger;
        }

        public List<Contacto> Lista()
        {
            DataTable dataTable = _contactoDAL.Lista();
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
                _logger.LogError("Error en la capa negocio: Contacto, metodo Lista" + ex.Message);
                throw new Exception(ex.Message);
            }
            return listaContactos;
        }
    }
}
