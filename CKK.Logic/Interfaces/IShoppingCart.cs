using CKK.Logic.Models;

namespace CKK.Logic.Interfaces
{
    
    public interface IShoppingCart
    {
        public int GetCustomerID();
        public ShoppingCartItem AddProduct(Product prod, int quant);
        public ShoppingCartItem RemoveProduct(int id, int quant);
        public decimal GetTotal();
        public ShoppingCartItem GetProductById(int id);
        public List<ShoppingCartItem> GetProducts();
    }
}
