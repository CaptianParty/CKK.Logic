using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Product : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        //HAVE NOT INITIATED THIS WILL NEED TO LATER FOR FINAL VERSION
        public bool IsActive { get; set; } = true;
    }
}
