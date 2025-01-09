namespace CKK.Logic.Exceptions
{
    [Serializable]
    public class ProductDoesNotExistException : Exception
    {
        public ProductDoesNotExistException() : base("This product does not exhist")
        {

        }

        public ProductDoesNotExistException(string message, Exception innerexception) : base(message, innerexception)
        {

        }
    }
}
