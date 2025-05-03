using CKK.Logic.Exceptions;

//DONT THINK I NEED THE INTERFACES IN CKK.LOGIC AT ALL
namespace CKK.Logic.Interfaces
{
    [Serializable]
    public abstract class Entity
    {
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                {
                    throw new InvalidIdException();
                }
                id = value;
            }
        }
        public string Name { get; set; }


    }
}