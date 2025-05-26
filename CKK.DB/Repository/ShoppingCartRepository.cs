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
            string sql = "INSERT INTO ShoppingCartItems (ShoppingCartId,ProductId,Quantity) VALUES (@ShoppingCartId, @ProductId, @Quantity)";
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

                var item = _productRepository.GetById(ProductId);

                var ProductItems = GetProducts(ShoppingCardId).Find(x => x.ProductId == ProductId);

                var shopitem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCardId,
                    ProductId = ProductId,
                    Quantity = quantity
                };

                if (item == null)
                {
                    throw new InvalidOperationException($"Product with ID {ProductId} not found.");
                }

                if (item.Quantity >= quantity)
                {
                    if (ProductItems != null)
                    {
                    //Product already in cart so update quantity
                    var test = UpdateAsync(shopitem);
                    }

                    else
                    {
                        //New product for the cart so add it
                        var test = AddAsync(shopitem);
                    }
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

        public decimal GetTotal(int shoppingCartId)
        {
            string sql = "SELECT SUM(items.Quantity * Price) FROM ShoppingCartItems items, Products prods WHERE items.ProductId = prods.Id AND ShoppingCartId = @ShoppingCartId";
            decimal result;
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                try
                {
                    result = connection.QuerySingleOrDefault<decimal>(sql, new { ShoppingCartId = shoppingCartId });
                }
                catch
                {
                    result = 0;
                }
            }
            return result;
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
            string sql = @"
    UPDATE ShoppingCartItems 
    SET 
        ShoppingCartId = @NewShoppingCartId, 
        ProductId = @NewProductId, 
        Quantity = @Quantity 
    WHERE 
        ProductId = @OldProductId 
        AND ShoppingCartId = @OldShoppingCartId";

            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, new
                {
                    NewShoppingCartId = entity.ShoppingCartId,
                    NewProductId = entity.ProductId,
                    Quantity = entity.Quantity,
                    OldProductId = entity.ProductId,
                    OldShoppingCartId = entity.ShoppingCartId
                });
                return result;
            }
        }

        public int UpdateAsync(ShoppingCartItem entity)
        {
            var sql = "UPDATE ShoppingCartItems SET ShoppingCartId = @ShoppingCartId, ProductId = @ProductId, Quantity = @Quantity WHERE ShoppingCartId = @ShoppingCartId AND ProductId = @ProductId";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public int AddAsync(ShoppingCartItem entity)
        {
            var sql = "Insert into ShoppingCartItems (ShoppingCartId,ProductId,Quantity) VALUES (@ShoppingCartId,@ProductId,@Quantity)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

    }
}

