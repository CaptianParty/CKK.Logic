using CKK.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.DB.Interfaces;
using CKK.DB.UOW;
using System.Data;
using Dapper;

namespace CKK.DB.Repository
{
    public class EmployeeRepository
    {
        private IConnectionFactory _connectionFactory;

        public EmployeeRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        public int AddEmployee(Employee employee)
        {
            string sql = "INSERT INTO Employee (FirstName, LastName, Email, Phone, Position, Password) " +
                          "VALUES (@FirstName, @LastName, @Email, @Phone, @Position, @Password) " +
                            "SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var newEmployeeNumber = connection.QuerySingle<int>(sql, employee);
                return newEmployeeNumber;
            }
        }
    }

    public class Employee
    {
        public int EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Password { get; set; }
    }
}
