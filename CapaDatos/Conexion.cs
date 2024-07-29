using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Conexion
    {
        public string cadena { get; set; }
        public Conexion()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            cadena = root.GetConnectionString("ConexionSql");
            //cadena = ConfigurationManager.ConnectionStrings["ConexionSql"].ConnectionString;
        }
    }
}
