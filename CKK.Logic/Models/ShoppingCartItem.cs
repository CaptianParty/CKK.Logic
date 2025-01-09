using CKK.Logic.Interfaces;
namespace CKK.Logic.Models
{
    [Serializable]
    public class ShoppingCartItem : InventoryItem
    {

        private ShoppingCartItem toy;
        private int v;



        public ShoppingCartItem(Product product, int quantity)
        {

            Product = product;
            Quantity = quantity;

        }

        public ShoppingCartItem(ShoppingCartItem toy, int v)
        {
            this.toy = toy;
            this.v = v;
        }


        public decimal GetTotal()
        {
            return Quantity * Product.Price;
        }


    }
}
