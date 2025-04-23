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
            throw new NotImplementedException();
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
        {//OR             List<ShoppingCartItem> cart = new List<ShoppingCartItem>();
            List<ShoppingCartItem> cart = GetAll();

            if (cart.Any(x => x.ShoppingCartId == shoppingCartId))
            {
                cart.RemoveAll(x => x.ShoppingCartId == shoppingCartId);
                return 1;
            }
            else
            {
                throw new Exception("Shopping cart already empty");
            }
        }

        public int Delete(int id)
        {
            List<ShoppingCartItem> cart = GetAll();

            ShoppingCartItem item = cart.FirstOrDefault(x => x.ShoppingCartId == id);

            if (item != null)
            {
                cart.Remove(item);
                return 1;
            }
            else
            {
                throw new Exception("Item not found");
            }
        }

        public List<ShoppingCartItem> GetAll()
        {//List<Order> orders = GET ALL();
            List<ShoppingCartItem> cart = new List<ShoppingCartItem>();

            foreach (var items in cart)
            {
                if (items != null)
                {
                    return cart;
                }
            }
            return GetAll();
        }

        public ShoppingCartItem GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItem> GetProducts(int shoppingCartId)
        {//OR             List<ShoppingCartItem> cart = new List<ShoppingCartItem>();

            List<ShoppingCartItem> cart = GetAll();

            var ProductsInCart = cart.Where(x => x.ShoppingCartId == shoppingCartId).ToList();

            if (ProductsInCart.Count > 0)
            {
                return ProductsInCart;
            }
            else
            {
                return new List<ShoppingCartItem>();
            }

        }

        public decimal GetTotal(int ShoppingCartId)
        {
            List<ShoppingCartItem> cart = GetProducts(ShoppingCartId);
            decimal total = cart.Sum(item => item.Quantity * item.Product.Price);
            return total;
        }

        public void Ordered(int shoppingCartId)
        {
            List<ShoppingCartItem> cart = GetProducts(shoppingCartId);
            decimal paid = cart.Sum(item => item.GetTotal());
            if (GetTotal(shoppingCartId) == paid)
            {
                Console.WriteLine("Your order has been placed");
                cart.Clear();
            }
            else
            {
                throw new Exception("Not all items are paid for");
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
