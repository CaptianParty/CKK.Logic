using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.DB.Interfaces;
using CKK.Logic.Models;
using Dapper;

namespace CKK.DB.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ProductRepository(IConnectionFactory Conn)
        {
            _connectionFactory = Conn;
        }

        public int Add(Product entity)
        {
            var sql = "Insert into Products (Price,Quantity,Name) VALUES (@Price,@Quantity,@Name)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }



        public int Delete(Product entity)
        {

            if (entity != null)
            {
                var sql = "DELETE FROM Products WHERE Id = @Id";
                using (var connection = _connectionFactory.GetConnection)
                {
                    connection.Open();
                    return connection.Execute(sql, new { Id = entity.Id });
                }
            }
            return 0;
        }

        public List<Product> GetAll()
        {
            var sql = "SELECT * FROM Products";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                return connection.Query<Product>(sql).AsList();
            }
        }

        public Product GetById(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
                return result;
            }
        }

        public List<Product> GetByName(string name)
        {
            string sql = "SELECT * FROM Products WHERE Name = @Name";
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Query<Product>(sql, new { Name = name }).ToList();
                return result;
            }
        }

        public int Update(Product entity)
        {
            var sql = "UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = connection.Execute(sql, entity);
                return result;
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<ShoppingCartItem> UpdateAsync(int quantity, int id)
        {
            var sql = "UPDATE Orders SET Quantity = @Quantity WHERE Id = @Id";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                await connection.ExecuteAsync(sql, new { Quantity = quantity, Id = id });

                var query = "SELECT * FROM Products WHERE Id = @Id";
                var result = await connection.QuerySingleOrDefaultAsync<ShoppingCartItem>(query, new { Id = id });
                return result;
            }
        }

        public async Task<ShoppingCartItem> AddAsync(int quantity, int id)
        {
            var sql = "INSERT INTO ShoppingCartItems (Quantity) VALUES (@Quantity)";
            using (var connection = _connectionFactory.GetConnection)
            {
                connection.Open();
                await connection.ExecuteAsync(sql, new { Quantity = quantity });
                var query = "SELECT * FROM Products WHERE Id = @Id";
                var result = await connection.QuerySingleOrDefaultAsync<ShoppingCartItem>(query, new { Id = id });
                return result;
            }
        }

        //NEW CODE NEED TO MAKE WORK

        //public List<Product> GetActiveProducts()
        //{
        //    var sql = "SELECT * FROM Products WHERE Quantity > 0 AND IsActive = 1 ";
        //    using (var connection = _connectionFactory.GetConnection)
        //    {
        //        connection.Open();
        //        return connection.Query<Product>(sql).ToList();
        //    }
        //}

    }
}
