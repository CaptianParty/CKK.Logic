using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


//FILE SAVING SLIGHTLY WORKS BUT I WILL NEED TO UPDATE IT WHEN I ADD THE EXTRA PAGE THATS ONLY FOR THE CART

namespace CKK.Persistance.Models
{
    public class FileStoring
    {
        int ShoppingCartIdCounter = 0;

        public static readonly string FilePath = Environment.GetFolderPath
            (Environment.SpecialFolder.MyDocuments) +
            Path.DirectorySeparatorChar + "Persistance" +
            Path.DirectorySeparatorChar + "ShoppingCart.json";

        public FileStoring()
        {
            CreatePath();
            Load();
        }

        private void CreatePath()
        {
            string dirPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Persistance"
            );
            Directory.CreateDirectory(dirPath);
        }

        public void SaveCart(List<ShoppingCartItem> items)
        {
            Items = items;
            Save();
        }

        public List<ShoppingCartItem> LoadCart()
        {
            Load();
            return Items;
        }



        private void Save()
        {
            if (Items.Count > 0)
                ShoppingCartIdCounter = Items.Max(x => x.ShoppingCartId) + 1;
            else
                ShoppingCartIdCounter = 1;
        }

        private void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var items = JsonSerializer.Deserialize<List<ShoppingCartItem>>(json);
                Items = items ?? new List<ShoppingCartItem>();
            }
        }

        private List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}

