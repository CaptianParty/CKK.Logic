using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class Store
    {
        private int _id;
        private string _name;
        private Product _product1;
        private Product _product2;
        private Product _product3;

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void AddStoreItem(Product product)
        {// null means uknown
            if (_product1 != null)
            {
                _product1 = product;
            }

            if (_product2 != null)
            {
                _product2 = product;
            }

            if (_product3 != null)
            {
                _product3 = product;
            }


        }
        public void RemoveStoreItem(int productNumber)
        {
            if (productNumber == 1)
            {
                _product1 = null;
            }

            if (productNumber == 0)
            {
                _product2 = null;
            }

            if (productNumber == 0)
            {
                _product3 = null;
            }
        }
    }
    public int GetStoreItems(int productNumber)
    {
       
    }
}
