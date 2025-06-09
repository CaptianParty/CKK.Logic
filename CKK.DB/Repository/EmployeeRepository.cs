using CKK.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.DB.Interfaces;
using CKK.DB.UOW;
using System.Data;

namespace CKK.DB.Repository
{
    public class EmployeeRepository
    {
        private IConnectionFactory _connectionFactory;

        public EmployeeRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        public int AddEmployee()
        {
            //START HERE    
            string sql = "INSERT INTO EmployeeInfo ()";

            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }
           
    }
}
