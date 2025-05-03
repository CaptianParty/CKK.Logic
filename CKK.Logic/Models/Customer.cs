using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    [Serializable]
    public class Customer : Entity
    {
        //WILL NEED TO IMPLEMENT THIS ON FINAL VERSION FOR MY OWN PROJECT BY ADDING A CHECKOUT WITH USER INPUT
    public int Id { get; set; }
    public string Name{ get; set; }
    public string Address { get; set; }
    public int ShoppingCartId { get; set; }
    }

}
