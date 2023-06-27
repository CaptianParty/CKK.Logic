
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

            if (productNumber == 2)
            {
                _product3 = null;
            }
        }
        public Product GetStoreItem(int productNumber)
        {
            if (productNumber == 1)
            {
                return (_product1);
            }

            if (productNumber == 2)
            {
                return _product2;
            }

            if (productNumber == 3)
            {
                return (_product3);
            }
            return null;

        }

        public Product FindStoreItemById(int id) 
        {
            if (_product1.GetId() == id)
            {
                return (_product1);
            }

            if (_product1.GetId() == id)
            {
                return _product2;
            }

            if (_product1.GetId() == id)
            {
                return (_product3);
            }
            return null;
        }

    }
   
}
