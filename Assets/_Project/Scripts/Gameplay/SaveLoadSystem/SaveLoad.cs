using System;
using System.IO;
using _Project.Scripts.Common.Services.SerializerDeserializer.Base;
using UnityEngine;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class SaveLoad : ISaveLoad
    {
        private readonly ISerializerDeserializer _serializerDeserializer;
        
        private string FilePath => Path.Combine(Application.persistentDataPath, "PlayerData.txt");

        public SaveLoad(ISerializerDeserializer serializerDeserializer)
        {
            _serializerDeserializer = serializerDeserializer;
            Debug.Log("Loading player data");
        } 

        public PlayerSaveLoadData Load()
        {
            if (File.Exists(FilePath) == false)
            {
                PlayerSaveLoadData initialData = new();
                initialData.AdsIsOff = false;
                initialData.DataTime = DateTime.Now;
                Save(initialData);
                return initialData;
            }
            return _serializerDeserializer.Deserialize<PlayerSaveLoadData>(File.ReadAllText(FilePath));
        }

        public void Save(PlayerSaveLoadData data) => 
            File.WriteAllText(FilePath, _serializerDeserializer.Serialize(data));
    }
}
