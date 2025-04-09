using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Product : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal price;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
