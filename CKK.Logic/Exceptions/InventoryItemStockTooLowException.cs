namespace CKK.Logic.Exceptions
{
    [Serializable]
    public class InventoryItemStockTooLowException : Exception
    {
        public InventoryItemStockTooLowException() : base("The inventory stock is to low")
        {

        }

        public InventoryItemStockTooLowException(string message, Exception innerException) : base(message, innerException) { }
    }
}
