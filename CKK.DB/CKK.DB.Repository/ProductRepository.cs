using CKK.DB.CKK.DB.Interfaces;
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.DB.CKK.DB.Repository
{
    public class ProductRepository : IProductRepository<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal price;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int Add(Product entity)
        {//List<Order> orders = GET ALL();
            List<Product> products = new List<Product>();
            if (entity != null)
            {
                products.Add(entity);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int id)
        {//List<Order> orders = GET ALL();
            List<Product> products = new List<Product>();
            var removeProduct = products.FirstOrDefault(o => o.Id == id);
            if (removeProduct != null)
            {
                products.Remove(removeProduct);
                return 1;
            }
            return 0;
        }

        public List<Product> GetAll()
        {//List<Order> orders = GET ALL();
            List<Product> products = new List<Product>();

            foreach (var product in products)
            {
                if (product == null) 
                { 
                    throw new ArgumentNullException(nameof(product), "Product can not be found.");
                }
            }
            return GetAll();
        }

        public Product GetById(int id)
        {//List<Order> orders = GET ALL();
            List<Product> products = new List<Product>();
            if(id == Id)
            {
                return products.FirstOrDefault(o => o.Id == id);
            }
            else
            {
                throw new Exception("Product ID does not exist.");
            }
        }

        public List<Product> GetByName(string name)
        {//List<Order> orders = GET ALL();
            List<Product> products = new List<Product>();
            var productName = products.Where(o => o.Name == name).ToList();
            if (productName.Any())
            {
                return productName;
            }
            else
            {
                throw new Exception("Product name does not exist.");
            }
        }

        public int Update(Product entity)
        {//List<Order> orders = GET ALL();
            List<Product> products = new List<Product>();

            if (entity != null)
            {
                var updateProduct = products.FirstOrDefault(o => o.Id == entity.Id);
                if(updateProduct != null)
                {
                    updateProduct.Name = entity.Name;
                    updateProduct.Price = entity.Price;
                    updateProduct.Quantity = entity.Quantity;
                }
               return Update(entity);
            }
            else
            {
                throw new ArgumentNullException(nameof(entity), "Product can not be found.");
            }
        }
    }
}
