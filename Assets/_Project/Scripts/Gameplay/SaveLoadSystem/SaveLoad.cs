using System.IO;
using UnityEngine;
using static Newtonsoft.Json.JsonConvert;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class SaveLoad
    {
        private readonly string _filePath;
        
        public SaveLoad() =>
            _filePath = Path.Combine(Application.persistentDataPath, "PlayerData.txt");
        
        public PlayerSaveLoadData Load()
        {
            if (File.Exists(_filePath) == false)
            {
                PlayerSaveLoadData initialData = new();
                Save(initialData);
                return initialData;
            }

            string playerSaveDataJson = File.ReadAllText(_filePath);
            return DeserializeObject<PlayerSaveLoadData>(playerSaveDataJson);
        }

        public void Save(PlayerSaveLoadData data)
        {
            string playerSaveLoadDataJson = SerializeObject(data);
            File.WriteAllText(_filePath, playerSaveLoadDataJson);
        }
    }
}
