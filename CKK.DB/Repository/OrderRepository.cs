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
                if (entity.PurchasedItems != null && entity.PurchasedItems.Any())
                    SavePurchasedItems(entity.OrderId, entity.PurchasedItems, connection);
                return result;
            }
        }

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
                var orders = connection.Query<Order>(sql).ToList();
                foreach (var order in orders)
                {
                    order.PurchasedItems = LoadPurchasedItems(order.OrderId, connection);
                }
                return orders;
            }
        }


        public Order GetById(int id)
        { 
            string sql = "SELECT * FROM Orders WHERE OrderId = @OrderId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var order = connection.QuerySingleOrDefault<Order>(sql, new { OrderId = id });
                if (order != null)
                    order.PurchasedItems = LoadPurchasedItems(order.OrderId, connection);
                return order;
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
                if (entity.PurchasedItems != null && entity.PurchasedItems.Any())
                    SavePurchasedItems(entity.OrderId, entity.PurchasedItems, connection);
                return result;
            }
        }

        private List<PurchasedItem> LoadPurchasedItems(int orderId, IDbConnection connection)
        {
            return connection.Query<PurchasedItem>(
                "SELECT ProductName, Quantity, PriceAtPurchase FROM PurchasedItems WHERE OrderId = @OrderId",
                new { OrderId = orderId }).ToList();
        }


        private void SavePurchasedItems(int orderId, List<PurchasedItem> items, IDbConnection connection)
        {
            connection.Execute("DELETE FROM PurchasedItems WHERE OrderId = @OrderId", new { OrderId = orderId });

            // Insert new items
            foreach (var item in items)
            {
                connection.Execute(
                    "INSERT INTO PurchasedItems (OrderId, ProductName, Quantity, PriceAtPurchase) VALUES (@OrderId, @ProductName, @Quantity, @PriceAtPurchase)",
                    new
                    {
                        OrderId = orderId,
                        ProductName = item.ProductName,
                        Quantity = item.Quantity,
                        PriceAtPurchase = item.PriceAtPurchase
                    });
            }
        }

    }
}
