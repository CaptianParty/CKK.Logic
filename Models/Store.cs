
using System.Security.Cryptography.X509Certificates;

namespace CKK.Logic.Models
{
    public class Store
    {
        private int _id;
        private string? _name;
        private Product? _product1;
        private Product? _product2;
        private Product? _product3;

        new 
            public List<StoreItem> item = new List<StoreItem>();
        public StoreItem AddItem(Product product, int quantity)
        {
            var x = new StoreItem(product, quantity);
                item.Add(x);
            return x;

            if(item == null)
            {
                item = new List<StoreItem>();
            }
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
            StoreItem item1 = new StoreItem(product, quantity);
            if (quantity < 0)
            {
                return null;
            }
            for (int x = 0; x < item.Count; x++)
            {
                if (item[x].GetProduct().GetId() == product.GetId())
                {
                    item[x].SetQuantity(item[x].GetQuantity() + quantity);
                    return item[x];
                }
               else
                {
                    //add new storeitem
                    item.Add(item1);
                    return item1;
                }
            }
            return AddStoreItem(product, quantity);
            


        }
        //FIGURE THIS OUT DUMMY

        public StoreItem RemoveStoreItem(int quantity, int id)
        { 
            StoreItem item1 = RemoveStoreItem(quantity, id);
            if (quantity < 0)
            {
                return null;
            }

            for (int x = 0; x< item.Count; x++)
            {
                {
                    if (id == item[x].GetProduct().GetId())
                    {
                        return item[x];
                    }

                    if (item[x].GetQuantity() < quantity)
                    {quantity = item[x].GetQuantity();
                        item[x].GetProduct().GetId();
                            return item[x];
                        }
                    
                }
            }
            return RemoveStoreItem(quantity, id);
        }
        public List<StoreItem>  GetStoreItem()
        {


            return item;

        }

        public Product FindStoreItemById(int id) 
        {
            if (_product1.GetId() == id)
            {
                return (_product1);
            }

            if (_product2.GetId() == id)
            {
                return _product2;
            }

            if (_product3.GetId() == id)
            {
                return (_product3);
            }
            return null;
        }

    }
   
}
