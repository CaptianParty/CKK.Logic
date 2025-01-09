using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Product : Entity
    {
        private decimal price;

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else { price = value; }

            }

        }

    }
}
