namespace CarStore.Communication.Response
{
    public class ResponseLoginUserJson
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public ResponseTokensJson Tokens { get; set; }
    }
}
