using System.IO;
using UnityEngine;
using static Newtonsoft.Json.JsonConvert;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class SaveLoad
    {
        private string FilePath => Path.Combine(Application.persistentDataPath, "PlayerData.txt");

        public PlayerSaveLoadData Load()
        {
            if (File.Exists(FilePath) == false)
            {
                PlayerSaveLoadData initialData = new();
                Save(initialData);
                return initialData;
            }
            string playerSaveDataJson = File.ReadAllText(FilePath);
            return DeserializeObject<PlayerSaveLoadData>(playerSaveDataJson);
        }

        public void Save(PlayerSaveLoadData data)
        {
            string playerSaveDataJson = SerializeObject(data);
            File.WriteAllText(FilePath, playerSaveDataJson);
        }
    }
}
