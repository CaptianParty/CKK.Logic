using CKK.Logic.Models;

namespace CKK.Logic.Interfaces
{
    
    public interface IStore
    {
        public StoreItem AddStoreItem(Product prod, int quantity);
        public StoreItem RemoveStoreItem(int id, int quantity);
        public StoreItem FindStoreItemById(int id);
        public StoreItem DeleteStoreItem(int id);
        public List<StoreItem> GetStoreItems();
        public List<StoreItem> GetAllProductsByName(string name);
        public List<StoreItem> GetProductsByQuantity(int quantity);
        public List<StoreItem> GetProductsByPrice(decimal price);
        public List<StoreItem> GetProductsById(int id);
    }
}
