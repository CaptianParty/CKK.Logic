using CKK.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.DB.Interfaces;
using CKK.Logic.Models;
using System.Xml.Linq;
using Dapper;
using System.Data;
using CKK.Logic.Interfaces;

namespace CKK.DB.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private IConnectionFactory _connectionFactory;
        public OrderRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        public int Add(Order entity)
        { 
            string sql = "INSERT INTO Orders (OrderId, OrderNumber, CustomerId, ShoppingCartId) VALUES (@OrderId, @OrderNumber, @CustomerId, @ShoppingCartId)";
            
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        //public int Delete(int entity)
        //{
        //    string sql = "DELETE FROM Orders WHERE OrderId = @OrderId";
        //    using (IDbConnection connection = _connectionFactory.GetConnection)
        //    {
        //        connection.Open();
        //        var result = connection.Execute(sql, new { OrderId = entity });
        //        return result;
        //    }
        //}

        //IMPLEMENTED NEW DELETE WORKS

        public int Delete(Order entity)
        {
            string sql = "DELETE FROM Orders WHERE OrderId = @OrderId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, new { OrderId = entity.OrderId });
                return result;
            }
        }

        public List<Order> GetAll()
        {
            string sql = "SELECT * FROM Orders";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Order>(sql).ToList();
                return result;
            }
        }


        public Order GetById(int id)
        { 
            string sql = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Order>(sql, new { OrderId = id });
                return result;
            }
        }

        
        public Order GetOrderByCustomerId(int customerId)
        {
            string sql = "SELECT * FROM Orders WHERE CustomerId = @CustomerId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Order>(sql);
                return result;
            }
        }

        public int Update(Order entity)
        {
            string sql = "UPDATE Orders SET OrderId = @OrderId, OrderNumber = @OrderNumber, CustomerId = @CustomerId, " +
                "ShoppingCartId = @ShoppingCartId WHERE OrderId = @OrderId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }
    }
}
