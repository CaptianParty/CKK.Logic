using System.Security.Cryptography.X509Certificates;

namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        private Customer? _Customer;
        private ShoppingCartItem _product1;
        private ShoppingCartItem _product2;
        private ShoppingCartItem _product3;

        public List<ShoppingCartItem> Products = new List<ShoppingCartItem>();

        
      public ShoppingCart(Customer cust)
        {
            _Customer = cust;

        }






        public int GetCustomerId(int _Customer)
        {
            return _Customer;
        }

       

        public ShoppingCartItem GetProductById(int id)
        {
            var f = Products.FirstOrDefault(x => x.GetProduct().GetId() == id);
            return f;
        }

        public ShoppingCartItem AddProduct(Product prod, int quantity)
        { if(quantity <= 0)
            {
                return null;
            }
            var f = Products.FirstOrDefault(x => x.GetProduct().GetId() == prod.GetId());
            if (f != null)
            {f.SetQuantity(f.GetQuantity() + quantity);
                return f;
            }

           
            var x = new ShoppingCartItem(prod, quantity);
             Products.Add(x);
            return x;
            
        }
        public ShoppingCartItem RemoveProduct(int id, int quantity)
        {
            var f = Products.FirstOrDefault(x => x.GetProduct().GetId() == id);
            if (f != null)
            {
                if (f.GetQuantity() - quantity > 0)
                {
                    f.SetQuantity(f.GetQuantity() - quantity);
                    return f;
                }

                f.SetQuantity(0);
                Products.Remove(f);
                return f;
            }
            

           return null;


            


        }
        public decimal GetTotal()
        {
            return Products.Sum(x => x.GetProduct().GetPrice()*x.GetQuantity());
           

            
        }
        public List<ShoppingCartItem> GetProducts()
        {
            return Products;


        }
        
    }
}
