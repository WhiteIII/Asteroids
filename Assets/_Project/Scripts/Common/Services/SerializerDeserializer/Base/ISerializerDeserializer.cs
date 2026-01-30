namespace _Project.Scripts.Common.Services.SerializerDeserializer.Base
{
    public interface ISerializerDeserializer
    {
        string Serialize(object serializableObject);
        T Deserialize<T>(string serializedObject);
    }
}
