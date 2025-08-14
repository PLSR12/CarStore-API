using System.Net;

namespace CarStore.Exceptions.ExceptionsBase
{
    public abstract class CarStoreException : SystemException
    {
        public CarStoreException(string message) : base(message) { }

        public abstract IList<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
