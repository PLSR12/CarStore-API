namespace CarStore.Infrastructure.Redis.Service
{
    // Só ajustar o nome da pasta e namespace conforme sua estrutura real
    public static class JsonSerializationExtension
    {
        public class PrivateSetterContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(
                System.Reflection.MemberInfo member,
                Newtonsoft.Json.MemberSerialization memberSerialization)
            {
                var prop = base.CreateProperty(member, memberSerialization);

                if (!prop.Writable)
                {
                    var property = member as System.Reflection.PropertyInfo;
                    if (property != null)
                    {
                        var hasPrivateSetter = property.GetSetMethod(true) != null;
                        prop.Writable = hasPrivateSetter;
                    }
                }

                return prop;
            }
        }
    }
}
