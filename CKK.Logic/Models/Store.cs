using CKK.Logic.Exceptions;
using CKK.Logic.Interfaces;


namespace CKK.Logic.Models
{

    public class Store : Entity, IStore
    {

        public List<StoreItem> Items = new List<StoreItem>();
        public StoreItem AddItem(Product product, int quantity)
        {
            var x = new StoreItem(product, quantity);
            Items.Add(x);


            if (Items == null)
            {
                Items = new List<StoreItem>();

            }

            return x;
        }


        public StoreItem AddStoreItem(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                throw new InventoryItemStockTooLowException();
            }
            var f = Items.FirstOrDefault(x => x.GetProduct().Id == product.Id);
            if (f != null)
            {
                f.SetQuantity(f.GetQuantity() + quantity);
                return f;
            }


            //NEW CODE
            if(product.Id == 0)
            {
                product.Id = product.Id++;
            }
            //END

            var x = new StoreItem(product, quantity);
            Items.Add(x);
            return x;
            


        }


        public StoreItem FindStoreItemById(int id)
        {

            if (id < 0)
            {
                throw new InvalidIdException();
            }

            var f = Items.FirstOrDefault(f => f.GetProduct().Id == id);
            return f;
        }

        public StoreItem RemoveStoreItem(int id, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }



            var f = FindStoreItemById(id);


            if (f != null)
            {
                if (f.GetQuantity() - quantity > 0)
                {
                    f.SetQuantity(f.GetQuantity() - quantity);
                    return f;
                }
                else if (f.GetQuantity() - quantity <= 0)

                {
                    f.SetQuantity(0);
                    return f;
                }

            }

            if (f == null)
            {
                throw new ProductDoesNotExistException();
            }

            return f;
        }

        public StoreItem DeleteStoreItem(int id)
        {
            var delete = DeleteStoreItem(id);
            if (delete != null)
            {
                Items.Remove(delete);
            }

            return delete;
        }

        public List<StoreItem> GetStoreItems()
        {

            return Items;
        }

        // NEW CODE Implementing the new methods

        public List<StoreItem> GetAllProductsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }

            if(name == char.MinValue.ToString() || name == char.MaxValue.ToString())
            {
                if (!Items.Any())
                {
                    return new List<StoreItem>();
                }

                name = Items.First().GetProduct().Name;
                return GetAllProductsByName(name);
            }

            return Items.Where(x => x.GetProduct().Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
            
        }

        public List<StoreItem> GetProductsByQuantity(int quantity)
        {
            var f = Items.Where(x => x.GetQuantity() == quantity).ToList();
            return f;
        }

        public List<StoreItem> GetProductsByPrice(decimal price)
        {
            var f = Items.Where(x => x.GetProduct().Price == price).ToList();
            return f;
        }

        public List<StoreItem> GetProductsById(int id)
        {
            var f = Items.Where(x => x.GetProduct().Id == id).ToList();
            return f;
        }
    }

}
