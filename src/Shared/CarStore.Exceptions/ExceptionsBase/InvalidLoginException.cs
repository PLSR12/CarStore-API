namespace CarStore.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : CarStoreException
    {
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
        {
        }
    }
}
