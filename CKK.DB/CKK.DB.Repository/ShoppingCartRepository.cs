using CKK.DB.CKK.DB.Interfaces;
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;
using CKK.Logic.Models;

namespace CKK.DB.CKK.DB.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository<ShoppingCartItem>
    {
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int quantity { get; set; }


        public int Add(ShoppingCartItem entity)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartItem AddToCart(int ShoppingCardId, int ProductId, int quantity)
        {//List<Order> orders = GET ALL();
            List<ShoppingCartItem> cart = new List<ShoppingCartItem>();

            ShoppingCartItem item = cart.FirstOrDefault(x => x.ShoppingCartId == ShoppingCardId && x.ProductId == ProductId);
            if (quantity > 0 && ProductId != null)
            {
                if(quantity >= item.Quantity)
                {
                    cart.Add(item);
                }
                return item;
            }
            else
            {
                throw new InventoryItemStockTooLowException();
            }
        }

        public int ClearCart(int shoppingCartId)
        {//OR             List<ShoppingCartItem> cart = new List<ShoppingCartItem>();
            List<ShoppingCartItem> cart = GetAll();
           
            if(cart.Any(x => x.ShoppingCartId == shoppingCartId))
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
        {//List<Order> orders = GET ALL();
            List<ShoppingCartItem> cart = GetProducts(ShoppingCartId);

            decimal total = cart.Sum(item => Product.Price * quantity);
            return total;
        }

        public void Ordered(int shoppingCartId)
        {//List<Order> orders = GET ALL();
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
        {//List<ShoppingCartItem> cart = new List<ShoppingCartItem>();

            List<ShoppingCartItem> cart = GetAll();

            if (entity != null)
            {
                var updateCart = cart.FirstOrDefault(o => o.ProductId == entity.ProductId);
                if (updateCart != null)
                {
                    if(updateCart.Quantity < cart.Count || updateCart.Quantity > cart.Count)
                    {
                        updateCart.Quantity = entity.Quantity;
                    }
                }
                return Update(entity);
            }
            return 0;
        }
    }
}
