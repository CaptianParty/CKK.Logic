using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using System.Text.Json;


namespace CKK.Persistance
{
    internal class FileStoring
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


        private void Save()
        {

            try
            {
                string json = JsonSerializer.Serialize(Items);
                File.WriteAllText(FilePath, json);
            }
            catch (IOException e)
            {
                throw new IOException("There was a problem writing to the file", e);
            }
            catch (SerializationException ex)
            {
                throw new SerializationException("There was a problem serializing the data: " + ex.Message, ex);
            }

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
