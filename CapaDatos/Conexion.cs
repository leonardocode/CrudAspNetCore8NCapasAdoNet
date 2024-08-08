using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Conexion
    {
        public string cadena { get; set; }

        SqlConnection conex;
        List<SqlParameter> listParametros;
        public string msjError;


        public Conexion()
        {
            cadena = "";
            msjError = "";

            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                var root = builder.Build();
                cadena = root.GetConnectionString("ConexionSql");

                this.listParametros = new List<SqlParameter>();
                this.conex = new SqlConnection(cadena);
                this.conex.Open();
            }
            catch (Exception ex)
            {
                this.msjError = ex.Message.ToString();
            }
        }

        /// <summary>
        /// Agrega un parámetro a la lista 
        /// </summary>
        /// <param name="nombre">Nombre Parámetro</param>
        /// <param name="valor">Valor parámetro</param>
        /// <param name="direccion">Dirección</param>
        /// <param name="nombreTipo">Tipo de dato</param>
        protected void adicionarParametro(string nombre, object valor, ParameterDirection direccion = ParameterDirection.Input, string nombreTipo = "")
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "@" + nombre;
            parametro.Value = valor;
            parametro.Direction = direccion;

            if (valor != null && valor.GetType() == typeof(DataTable))
            {
                parametro.SqlDbType = SqlDbType.Structured;

            }

            if (nombreTipo != "")
            {
                parametro.TypeName = nombreTipo;
            }

            this.listParametros.Add(parametro);

        }



        protected async Task<DataSet> ejecutarProcedimiento(string nombreProcedimiento)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = this.conex;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = nombreProcedimiento;
                comando.CommandTimeout = 900;
                foreach (SqlParameter parametro in listParametros)
                {
                    comando.Parameters.AddWithValue(parametro.ParameterName, parametro.Value);
                }


                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = comando;
                await Task.Run(() => da.Fill(ds));

                //await comando.ExecuteNonQueryAsync()
            }
            catch (Exception ex)
            {
                this.msjError = ex.Message.ToString();
            }
            finally
            {
                this.conex.Close();
                this.listParametros.Clear();
            }

            return ds;
        }

        protected DataSet ejecutarProcedimientoSync(string nombreProcedimiento)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = this.conex;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = nombreProcedimiento;
                comando.CommandTimeout = 900;
                foreach (SqlParameter parametro in listParametros)
                {
                    comando.Parameters.AddWithValue(parametro.ParameterName, parametro.Value);
                }



                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = comando;
                da.Fill(ds);

                //await comando.ExecuteNonQueryAsync()
            }
            catch (Exception ex)
            {
                this.msjError = ex.Message.ToString();
            }
            finally
            {
                this.conex.Close();
                this.listParametros.Clear();
            }

            return ds;
        }

    }
}
