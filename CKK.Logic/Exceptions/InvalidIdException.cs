namespace CKK.Logic.Exceptions
{
    [Serializable]
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("Not a valid Id")
        {

        }

        public InvalidIdException(string message, Exception innerexception) : base(message, innerexception)
        {

        }




    }
}
