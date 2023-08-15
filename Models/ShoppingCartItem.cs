using System.Collections.Generic;
using System.Linq;
namespace CKK.Logic.Models
{
    public class ShoppingCartItem
    {
        private Product _product;
        private int _quantity;
        private ShoppingCartItem toy;
        private int v;

        

        public ShoppingCartItem(Product product, int quantity) 
        {
            _product = product;
            _quantity = quantity;

        }

        public ShoppingCartItem(ShoppingCartItem toy, int v)
        {
            this.toy = toy;
            this.v = v;
        }

        public int GetQuantity()
        { return _quantity; }
        public void SetQuantity(int quantity)
        { _quantity = quantity; }
        

        public Product GetProduct() 
        { return _product; }
        public void SetProduct(Product product)
        {
            _product = product;
        }
       public decimal GetTotal()
        {
            return _quantity * _product.GetPrice();
        }

        
    }
}
