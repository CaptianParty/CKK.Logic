using CKK.Logic.Exceptions;
using CKK.Logic.Models;


namespace CKK.Logic.Interfaces
{
    [Serializable]
    public abstract class InventoryItem
    {
        private int quantity;
        public Product Product { get; set; }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value < 0)
                {
                    throw new InventoryItemStockTooLowException();
                }
                quantity = value;
            }

        }
    }
}
