using System.Net;

namespace CarStore.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : CarStoreException
    {
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
