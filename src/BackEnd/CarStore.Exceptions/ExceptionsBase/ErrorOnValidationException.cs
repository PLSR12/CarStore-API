using System.Net;

namespace CarStore.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : CarStoreException
    {
        public IList<string> ErrorsMessages { get; set; }
        public ErrorOnValidationException(IList<string> errorsMessages) : base(string.Empty)
        {
            ErrorsMessages = errorsMessages;
        }

        public override IList<string> GetErrorMessages() => ErrorsMessages;
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
