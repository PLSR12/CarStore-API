using System.Net;

namespace CarStore.Exceptions.ExceptionsBase
{
    public class InvalidTokenException : CarStoreException
    {
        public InvalidTokenException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
