using System.IO;
using _Project.Scripts.Common.Services.SerializerDeserializer.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class SaveLoad : ISaveLoad
    {
        private readonly ISerializerDeserializer _serializerDeserializer;
        
        private string FilePath => Path.Combine(Application.persistentDataPath, "PlayerData.txt");

        public SaveLoad(ISerializerDeserializer serializerDeserializer) =>
            _serializerDeserializer = serializerDeserializer;

        public PlayerSaveLoadData Load()
        {
            if (File.Exists(FilePath) == false)
            {
                PlayerSaveLoadData initialData = new();
                initialData.AdsIsOff = false;
                Save(initialData);
                return initialData;
            }
            string playerSaveDataJson = File.ReadAllText(FilePath);
            return _serializerDeserializer.Deserialize<PlayerSaveLoadData>(playerSaveDataJson);
        }

        public void Save(PlayerSaveLoadData data)
        {
            string playerSaveDataJson = _serializerDeserializer.Serialize(data);
            File.WriteAllText(FilePath, playerSaveDataJson);
        }
    }
}
