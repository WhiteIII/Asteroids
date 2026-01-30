using _Project.Scripts.Common.Services.SerializerDeserializer.Base;
using static Newtonsoft.Json.JsonConvert;

namespace _Project.Scripts.Common.Services.SerializerDeserializer.Implementation
{
    public class NewtonsoftSerializerDeserializer : ISerializerDeserializer
    {
        public string Serialize(object serializableObject) => 
            SerializeObject(serializableObject);

        public T Deserialize<T>(string serializedObject) => 
            DeserializeObject<T>(serializedObject);
    }
}