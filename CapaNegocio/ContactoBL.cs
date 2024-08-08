using CapaDatos;
using CapaEntidades;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
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


        #region Listar
        public async Task<(List<Contacto> Contactos, string ErrorMessage)> Lista()
        {
            try
            {
                var dtDatos = await _contactoDAL.Lista();

                if (!string.IsNullOrEmpty(_contactoDAL.ErrorMessage))
                {
                    return (null, _contactoDAL.ErrorMessage);
                }

                if (dtDatos == null || dtDatos.Rows.Count == 0)
                {
                    return (new List<Contacto>(), string.Empty);
                }

                List<Contacto> contactos = new List<Contacto>();
                foreach (DataRow row in dtDatos.Rows)
                {
                    contactos.Add(new Contacto
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Nombre = row["Nombre"].ToString(),
                        Celular = row["Celular"].ToString(),
                        Email = row["Email"].ToString(),
                        Estado = Convert.ToBoolean(row["Estado"])
                    });
                }

                return (contactos, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa negocio: Contacto, método Lista");
                return (null, $"Error al listar los contactos: {ex.Message}");
            }
        }
        #endregion Listar

        #region ObtenerPorId
        public async Task<(Contacto Contacto, string ErrorMessage)> ObtenerPorId(int id)
        {
            try
            {
                var dtDatos = await _contactoDAL.ObtenerPorId(id);

                if (!string.IsNullOrEmpty(_contactoDAL.ErrorMessage))
                {
                    return (null, _contactoDAL.ErrorMessage);
                }

                if (dtDatos == null || dtDatos.Rows.Count == 0)
                {
                    return (null, $"No se encontró el contacto con ID {id}");
                }

                DataRow row = dtDatos.Rows[0];
                Contacto contacto = new Contacto
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString(),
                    Celular = row["Celular"].ToString(),
                    Email = row["Email"].ToString(),
                    Estado = Convert.ToBoolean(row["Estado"])
                };

                return (contacto, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa negocio: Contacto, método GetById");
                return (null, $"Error al obtener el contacto: {ex.Message}");
            }
        }
        #endregion ObtenerPorId

        #region Insertar
        public async Task<(bool Success, string ErrorMessage)> Insertar(Contacto contacto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contacto.Nombre))
                    return (false, "El campo nombre es obligatorio");

                if (string.IsNullOrWhiteSpace(contacto.Celular))
                    return (false, "El campo celular es obligatorio");

                if (string.IsNullOrWhiteSpace(contacto.Email))
                    return (false, "El campo email es obligatorio");

                var dtDatos = await _contactoDAL.Insertar(contacto);

                if (!string.IsNullOrEmpty(_contactoDAL.ErrorMessage))
                {
                    return (false, _contactoDAL.ErrorMessage);
                }

                if (dtDatos == null || dtDatos.Rows.Count == 0)
                {
                    return (false, "No se pudo insertar el contacto");
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa negocio: Contacto, método Insertar");
                return (false, $"Error al insertar el contacto: {ex.Message}");
            }
        }
        #endregion Insertar

        #region Actualizar
        public async Task<(bool Success, string ErrorMessage)> Actualizar(Contacto contacto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contacto.Nombre))
                    return (false, "El campo nombre es obligatorio");

                if (string.IsNullOrWhiteSpace(contacto.Celular))
                    return (false, "El campo celular es obligatorio");

                if (string.IsNullOrWhiteSpace(contacto.Email))
                    return (false, "El campo email es obligatorio");

                var dtDatos = await _contactoDAL.Actualizar(contacto);

                if (!string.IsNullOrEmpty(_contactoDAL.ErrorMessage))
                {
                    return (false, _contactoDAL.ErrorMessage);
                }

                if (dtDatos == null || dtDatos.Rows.Count == 0)
                {
                    return (false, "No se pudo actualizar el contacto");
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa negocio: Contacto, método Actualizar");
                return (false, $"Error al actualizar el contacto: {ex.Message}");
            }
        }
        #endregion Actualizar

        #region Eliminar
        public async Task<(bool Success, string ErrorMessage)> Eliminar(int id)
        {
            try
            {
                var dtDatos = await _contactoDAL.Eliminar(id);

                if (!string.IsNullOrEmpty(_contactoDAL.ErrorMessage))
                {
                    return (false, _contactoDAL.ErrorMessage);
                }

                if (dtDatos == null || dtDatos.Rows.Count == 0)
                {
                    return (false, "No se pudo eliminar el contacto");
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la capa negocio: Contacto, método Eliminar");
                return (false, $"Error al eliminar el contacto: {ex.Message}");
            }
        }
        #endregion Eliminar
    }
}
