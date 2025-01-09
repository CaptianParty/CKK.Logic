using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Exceptions;
using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Interfaces;

namespace CKK.Persistance.Models
{
    [Serializable]
    public class FileStore : IStore, ISavable, ILoadable
    {

        #pragma warning disable SYSLIB0011

        private List<StoreItem> Items;


        public static readonly string FilePath = Environment.GetFolderPath
            (Environment.SpecialFolder.MyDocuments) +
            Path.DirectorySeparatorChar + "Persistance" +
            Path.DirectorySeparatorChar + "StoreItems1.dat";
        private int IdCounter = 0;
        public FileStore()
        {
            CreatePath();
            Items = new();
            Load();
        }

            /*COME BACK TO THIS*/
        private void CreatePath()
        {
            Directory.CreateDirectory(Environment.GetFolderPath
                (Environment.SpecialFolder.MyDocuments) 
                +Path.DirectorySeparatorChar + "Persistance" +
                Path.DirectorySeparatorChar + "StoreItems.dat");
        }


        public void Save()
        {
            FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);

            try
            {

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, Items);
            }
            catch(IOException e) 
            {
                throw new IOException("There was a problem writing to the file", e);
            }     
            catch(SerializationException ex)
            {
                throw new SerializationException("There was a problem serializing the data: " + ex.Message, ex);
            }
            finally
            {
                stream?.Dispose();
            }

            IdCounter = Items.Max(x => x.Product.Id);
        }

        public void Load()
        {
            FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Read);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Items = (List<StoreItem>)formatter.Deserialize(stream);
                IdCounter = Items.Max(x => x.Product.Id);

            }
            catch(IOException e)
            {
                throw new IOException("There has been an error opening the file to load data", e);
            }
            catch(SerializationException ex)
            {
                Items = new();
            }
            finally 
            { 
                stream?.Dispose(); 
            }
        }

        public StoreItem AddStoreItem(Product prod, int quantity)
        {
           if ( quantity < 0)
            {
                throw new InventoryItemStockTooLowException();
            }
           if (prod.Id == 0)
            {
                prod.Id = 
                    ++IdCounter;
            }

           var existingItem = FindStoreItemById( prod.Id );
            if (existingItem == null)
            {
                StoreItem storeItem = new StoreItem( prod, quantity );
                Items.Add( storeItem );
                Save();
                return storeItem;
            }
            else
            {
                existingItem.SetQuantity(existingItem.GetQuantity() + quantity);
                Save(); 
                return existingItem;
            }
        }
        
        public StoreItem RemoveStoreItem(int id, int quantity)
        {
            var existingItem = FindStoreItemById(id);
            if (existingItem != null)
            {
                if (existingItem.GetQuantity() - quantity < 0)
                {
                    existingItem.SetQuantity(0);
                }
                else
                {
                    existingItem.SetQuantity(existingItem.GetQuantity() - quantity);
                }
                Save();
                return existingItem;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
            
        }

        
        public StoreItem FindStoreItemById(int id)
        {
            if (id < 0)
            {
                throw new InvalidIdException();
            }

            foreach (var item in Items)
            {
                if (item.Product != null && item.Product.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public StoreItem DeleteStoreItem(int id)
        {
            var existingItem = FindStoreItemById(id);
            if (existingItem != null)
            {
                Items.Remove(existingItem);
            }
            Save();
            return existingItem;
        }
        public List<StoreItem> GetStoreItems()
        {
            return Items;
        }

        //TODO: Implement the following methods in the FileStore class

        public List<StoreItem> GetAllProductsByName(string name)
        {
            List<StoreItem> items = new();
            foreach (var item in Items)
            {
                if (item.Product.Name == name)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        public List<StoreItem> GetProductsByQuantity(int quantity)
        {
            List<StoreItem> items = new();
            foreach (var item in Items)
            {
                if (item.Quantity == quantity)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        public List<StoreItem> GetProductsByPrice(decimal price)
        {
            List<StoreItem> items = new();
            foreach (var item in Items)
            {
                if (item.Product.Price == price)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        public List<StoreItem> GetProductsById(int id)
        {
            List<StoreItem> items = new();
            foreach (var item in Items)
            {
                if (item.Product.Id == id)
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
