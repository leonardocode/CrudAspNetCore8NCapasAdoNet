using CapaEntidades;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ContactoDAL : Conexion
    {
        private readonly ILogger<ContactoDAL> _logger;
        public string ErrorMessage { get; private set; }
        public ContactoDAL(ILogger<ContactoDAL> logger)
        {
            this._logger = logger;
        }



        //#region usandoSelectQuemado
        ////     public List<Contacto> Lista()
        ////     {
        ////         List<Contacto> lista = new List<Contacto>();
        ////         try
        ////{
        ////	using(SqlConnection conexion = new SqlConnection(cadena))
        ////	{
        ////		try
        ////		{
        ////                     conexion.Open();
        ////			using (SqlCommand cmd = new SqlCommand("Select * from CONTACTO where Estado = 1", conexion))
        ////			{
        ////				cmd.CommandType = CommandType.Text;
        ////			    SqlDataReader drd =	cmd.ExecuteReader();
        ////				if(drd != null)
        ////				{
        ////					Contacto ocontacto;
        ////					lista = new List<Contacto>();
        ////					while(drd.Read())
        ////					{
        ////						ocontacto = new Contacto();
        ////						ocontacto.Id = drd.GetInt32(0);
        ////						ocontacto.Nombre = drd.GetString(1);
        ////						ocontacto.Celular = drd.GetString(2);
        ////						ocontacto.Email = drd.GetString(3);
        ////						ocontacto.Estado = drd.GetBoolean(4);
        ////						lista.Add(ocontacto);
        ////					}

        ////				}
        ////			}
        ////                 }
        ////		catch (Exception ex)
        ////		{
        ////                     conexion.Close();
        ////                     lista = null;
        ////                     throw new Exception("Error en la conexion capa datos, metodo lista contacto:" + ex.Message);
        ////		}
        ////	}
        ////}
        ////catch (Exception ex)
        ////{
        ////	lista = null;
        ////	throw new Exception("Error en la capa datos, metodo lista contacto: " +ex.Message);
        ////}
        ////return lista;
        ////     }
        //#endregion usandoSelectQuemado

       

        #region Listar
        public async Task<DataTable> Lista()
        {
            DataTable dtDatos = new DataTable();
            ErrorMessage = string.Empty;

            try
            {
                this.adicionarParametro("Opcion", 1);
                DataSet ds = await ejecutarProcedimiento("sp_crudAdoNet");

                if (ds == null || ds.Tables.Count == 0)
                {
                    ErrorMessage = "No se obtuvieron resultados al listar los contactos.";
                    _logger.LogWarning(ErrorMessage);
                    return null;
                }

                dtDatos = ds.Tables[0];
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al listar contactos: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
                return null;
            }

            return dtDatos;
        }
        #endregion Listar


        #region ObtenerPorId
        public async Task<DataTable> ObtenerPorId(int id)
        {
            DataTable dtDatos = new DataTable();
            ErrorMessage = string.Empty;

            try
            {
                this.adicionarParametro("Opcion", 2);
                this.adicionarParametro("id", id);
                DataSet ds = await ejecutarProcedimiento("sp_crudAdoNet");

                if (ds == null || ds.Tables.Count == 0)
                {
                    ErrorMessage = $"No se encontró el contacto con ID {id}.";
                    _logger.LogWarning(ErrorMessage);
                    return null;
                }

                dtDatos = ds.Tables[0];
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al obtener el contacto: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
                return null;
            }

            return dtDatos;
        }
        #endregion ObtenerPorId


        #region Insertar
        public async Task<DataTable> Insertar(Contacto contacto)
        {
            DataTable dtDatos = new DataTable();
            ErrorMessage = string.Empty;

            try
            {
                this.adicionarParametro("Opcion", 3);
                this.adicionarParametro("nombre", contacto.Nombre);
                this.adicionarParametro("celular", contacto.Celular);
                this.adicionarParametro("email", contacto.Email);
                this.adicionarParametro("usuarioCreacion", "admin");
                this.adicionarParametro("usuarioModificacion", "admin");

                DataSet ds = await ejecutarProcedimiento("sp_crudAdoNet");

                if (ds == null || ds.Tables.Count == 0)
                {
                    ErrorMessage = "No se obtuvieron resultados al insertar el contacto.";
                    _logger.LogWarning(ErrorMessage);
                    return null;
                }

                dtDatos = ds.Tables[0];

                if (dtDatos.Rows.Count > 0 && dtDatos.Columns.Contains("Respuesta"))
                {
                    string respuesta = dtDatos.Rows[0]["Respuesta"].ToString();
                    if (respuesta != "OK")
                    {
                        ErrorMessage = respuesta;
                        _logger.LogWarning($"Error al insertar contacto: {ErrorMessage}");
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorMessage = $"Error de SQL al insertar contacto: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al insertar contacto: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
                return null;
            }

            return dtDatos;
        }
        #endregion Insertar


        #region Actualizar
        public async Task<DataTable> Actualizar(Contacto contacto)
        {
            DataTable dtDatos = new DataTable();
            ErrorMessage = string.Empty;

            try
            {
                this.adicionarParametro("Opcion", 4);
                this.adicionarParametro("id", contacto.Id);
                this.adicionarParametro("nombre", contacto.Nombre);
                this.adicionarParametro("celular", contacto.Celular);
                this.adicionarParametro("email", contacto.Email);
                this.adicionarParametro("estado", contacto.Estado);
                this.adicionarParametro("usuarioModificacion", "admin");

                DataSet ds = await ejecutarProcedimiento("sp_crudAdoNet");

                if (ds == null || ds.Tables.Count == 0)
                {
                    ErrorMessage = "No se obtuvieron resultados al actualizar el contacto.";
                    _logger.LogWarning(ErrorMessage);
                    return null;
                }

                dtDatos = ds.Tables[0];

                if (dtDatos.Rows.Count > 0 && dtDatos.Columns.Contains("Respuesta"))
                {
                    string respuesta = dtDatos.Rows[0]["Respuesta"].ToString();
                    if (respuesta != "OK")
                    {
                        ErrorMessage = respuesta;
                        _logger.LogWarning($"Error al actualizar contacto: {ErrorMessage}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al actualizar contacto: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
                return null;
            }

            return dtDatos;
        }
        #endregion Actualizar


        #region Eliminar
        public async Task<DataTable> Eliminar(int id)
        {
            DataTable dtDatos = new DataTable();
            ErrorMessage = string.Empty;

            try
            {
                this.adicionarParametro("Opcion", 5);
                this.adicionarParametro("id", id);

                DataSet ds = await ejecutarProcedimiento("sp_crudAdoNet");

                if (ds == null || ds.Tables.Count == 0)
                {
                    ErrorMessage = "No se obtuvieron resultados al eliminar el contacto.";
                    _logger.LogWarning(ErrorMessage);
                    return null;
                }

                dtDatos = ds.Tables[0];

                if (dtDatos.Rows.Count > 0 && dtDatos.Columns.Contains("Respuesta"))
                {
                    string respuesta = dtDatos.Rows[0]["Respuesta"].ToString();
                    if (respuesta != "OK")
                    {
                        ErrorMessage = respuesta;
                        _logger.LogWarning($"Error al eliminar contacto: {ErrorMessage}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al eliminar contacto: {ex.Message}";
                _logger.LogError(ex, ErrorMessage);
                return null;
            }

            return dtDatos;
        }
        #endregion Eliminar
    }
}
