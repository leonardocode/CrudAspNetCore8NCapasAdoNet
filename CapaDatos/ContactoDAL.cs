using CapaEntidades;
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

        //#region Listar
        //public List<Contacto> Lista()
        //{
        //    List<Contacto> lista = new List<Contacto>();
        //    try
        //    {
        //        using (SqlConnection conexion = new SqlConnection(cadena))
        //        {
        //            try
        //            {
        //                conexion.Open();
        //                using (SqlCommand cmd = new SqlCommand("sp_crudAdoNet", conexion))
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    //cmd.Parameters.AddWithValue("@nombreParametro", nombreParametro);
        //                    SqlDataReader drd = cmd.ExecuteReader();
        //                    if (drd != null)
        //                    {
        //                        Contacto ocontacto;
        //                        lista = new List<Contacto>();
        //                        while (drd.Read())
        //                        {
        //                            ocontacto = new Contacto();
        //                            ocontacto.Id = drd.GetInt32(0);
        //                            ocontacto.Nombre = drd.GetString(1);
        //                            ocontacto.Celular = drd.GetString(2);
        //                            ocontacto.Email = drd.GetString(3);
        //                            ocontacto.Estado = drd.GetBoolean(4);
        //                            lista.Add(ocontacto);
        //                        }

        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                conexion.Close();
        //                lista = null;
        //                throw new Exception("Error en la conexion capa datos, metodo lista contacto:" + ex.Message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lista = null;
        //        throw new Exception("Error en la capa datos, metodo lista contacto: " + ex.Message);
        //    }
        //    return lista;
        //}
        //#endregion Listar

        public DataTable Lista()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    try
                    {
                        conexion.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_crudAdoNet", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Parameters.AddWithValue("@nombreParametro", nombreParametro);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                            dataAdapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        conexion.Close();
                        dataTable = null;
                        throw new Exception("Error en la conexion capa datos, metodo lista contacto:" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                dataTable = null;
                throw new Exception("Error en la capa datos, metodo lista contacto: " + ex.Message);
            }
            return dataTable;
        }

    }
}
