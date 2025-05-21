namespace CarStore.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : CarStoreException
    {
        public IList<string> ErrorsMessages { get; set; }
        public ErrorOnValidationException(IList<string> errorsMessages) : base(string.Empty)
        {
            ErrorsMessages = errorsMessages;
        }
    }
}
