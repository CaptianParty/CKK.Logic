
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
namespace CKK.Logic.Models
{
    public class Store
    {
        private int _id;
        private string? _name;
        private Product? _product1;
        private Product? _product2;
        private Product? _product3;

        
            public List<StoreItem> item = new List<StoreItem>();
        public StoreItem AddItem(Product product, int quantity)
        {
            var x = new StoreItem(product, quantity);
                item.Add(x);
            

            if(item == null)
            {
                item = new List<StoreItem>();
                
            }

            return x;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public string? GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }
                //void
        public StoreItem AddStoreItem(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                return null;
            }
            var f = item.FirstOrDefault(x => x.GetProduct().GetId() == product.GetId());
            if (f != null)
            {
                f.SetQuantity(f.GetQuantity() + quantity);
                return f;
            }


            var x = new StoreItem(product, quantity);
            item.Add(x);
            return x;



        }


        public StoreItem RemoveStoreItem(int id, int quantity )
        { 
            
            if (quantity <= 0)
            {
                
                return null;
            }

            for (int x = 0; x< item.Count; x++)
            {
                {
                    var f = item.FirstOrDefault(x => x.GetProduct().GetId() == id);
                    if (f != null)
                    {
                        if (f.GetQuantity() - quantity > 0)
                        {
                            f.SetQuantity(f.GetQuantity() - quantity);
                            return f;
                        }

                        f.SetQuantity(0);
                        item.Remove(f);
                        return f;
                    }

                }
            }
            return null;
        }
        public List<StoreItem> GetStoreItems()
        {
            return item;

        }

        public StoreItem FindStoreItemById(int id) 
        {
            var f = item.FirstOrDefault(f => f.GetProduct().GetId() == id);
            return f;
        }

    }
   
}
