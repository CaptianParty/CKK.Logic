using CKK.Logic.Exceptions;
using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private Customer? _Customer;
        public List<ShoppingCartItem> Products { get; set; } = new List<ShoppingCartItem>();


        public ShoppingCart(Customer cust)
        {
            _Customer = cust;

        }










        public ShoppingCartItem GetProductById(int id)
        {
            if (id < 0)
            {
                throw new InvalidIdException();
            }
            var f = Products.FirstOrDefault(x => x.Product.Id == id);
            return f;
        }

        public ShoppingCartItem AddProduct(Product prod, int quantity)
        {
            if (quantity < 0)
            {
                throw new InventoryItemStockTooLowException();
            }
            var f = Products.FirstOrDefault(x => x.Product.Id == prod.Id);
            if (f != null)
            {
                f.Quantity += quantity;
                return f;
            }


            var x = new ShoppingCartItem(prod, quantity);
            Products.Add(x);
            return x;

        }
        public ShoppingCartItem RemoveProduct(int id, int quantity)
        {
            var f = Products.FirstOrDefault(x => x.Product.Id == id);
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (f != null)
            {
                if (f.Quantity - quantity > 0)
                {
                    f.Quantity -= quantity;
                    return f;
                }


                f.Quantity = 0;
                Products.Remove(f);
                return f;

            }
            throw new ProductDoesNotExistException();
        }
        public decimal GetTotal()
        {
            return Products.Sum(x => x.Product.Price * x.Quantity);



        }
        /* public decimal GetTotal()
         {
             decimal Sum; 

             var f = Products.FirstOrDefault(x=>x.Product.Price == 0);
            Sum = f.Product.Price *= f.Quantity;
             return Sum;
         }*/

        public int GetCustomerID()
        {
            return _Customer.Id;
        }

        public List<ShoppingCartItem> GetProducts()
        {

            return Products;
        }




    }
}
