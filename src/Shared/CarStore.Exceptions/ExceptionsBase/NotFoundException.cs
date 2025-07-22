using System.Net;

namespace CarStore.Exceptions.ExceptionsBase
{
    public class NotFoundException : CarStoreException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
