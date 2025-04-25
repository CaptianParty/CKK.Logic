using CKK.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CKK.DB.UOW
{
    public class DatabaseConnectionFactory : IConnectionFactory
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        private readonly string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = StructuredProjectDB"; 
            
        public IDbConnection GetConnection
        {
            get
            {
                
                
                DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", Microsoft.Data.SqlClient.SqlClientFactory.Instance);
                var factory = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");
                var conn = factory.CreateConnection();
                conn.ConnectionString = connectionString;
                return conn;
            }
        }
    }
}
