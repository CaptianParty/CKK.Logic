using CKK.DB.Interfaces;
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;
using CKK.Logic.Models;
using System.Xml.Linq;
using Dapper;
using CKK.DB.Repository;
using System.Data;

namespace CKK.DB.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public ShoppingCartRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        public IConnectionFactory Conn { get; }

        public int Add(ShoppingCartItem entity)
        {
            string sql = "INSERT INTO ShoppingCartItems (ShoppingCartId,ProductId,Quantity) VALUES (ShoppingCartId = " +
                "@ShoppingCartId, ProductId = @ProductId, Quantity = @Quantity)";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public ShoppingCartItem AddToCart(int ShoppingCardId, int ProductId, int quantity)
        {
            using (var conn = _connectionFactory.GetConnection)
            {
                ProductRepository _productRepository = new ProductRepository(_connectionFactory);

                var item = _productRepository.GetByIdAsync(ProductId).Result;

                var ProductItems = GetProducts(ShoppingCardId).Find(x => x.ProductId == ProductId);

                var shopitem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCardId,
                    ProductId = ProductId,
                    Quantity = quantity
                };

                if (item.Quantity >= quantity)
                {
                    //Product already in cart so update quantity
                    var test = _productRepository.UpdateAsync(ProductId, quantity);
                }
                else
                {
                    //New product for the cart so add it
                    var test = _productRepository.AddAsync(ProductId, quantity);
                }
                return shopitem;
            }
        }

        public int ClearCart(int shoppingCartId)
        {
            string sql = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, new { ShoppingCartId = shoppingCartId });
                return result;
            }
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {
            string sql = "SELECT * FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<ShoppingCartItem>(sql, new { ShoppingCartId = shoppingCartId }).ToList();
                return result;
            }

        }

        public decimal GetTotal(int ShoppingCartId)
        {
            string sql = "SELECT SUM(Quantity) FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<decimal>(sql, new { ShoppingCartId = ShoppingCartId });
                return result;
            }
        }

        public void Ordered(int shoppingCartId)
        {
            string sql = "DELETE FROM ShoppingCartItems WHERE ShoppingCartId = @ShoppingCartId";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                connection.Execute(sql, new { ShoppingCartId = shoppingCartId });
            }
        }

        public int Update(ShoppingCartItem entity)
        {
            var sql = "UPDATE ShoppingCartItems SET ShoppingCartId = @ShoppingCartId, ProductId = @ProductId," +
                "Quantity = @Quantity WHERE ShoppingCartId = @ShoppingCartId AND Product = @ProductId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }
    }
}
