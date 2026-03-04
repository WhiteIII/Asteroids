#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using _Project.Scripts.Gameplay.SaveLoadSystem;
using Unity.Services.CloudSave;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Common.Services.Cheats
{
    public class SaveLoadDebugHelper
    {
        private const string REMOTE_SAVE_FILE_KEY = "base_save_data";
            
        private static string SaveFilePath => Path.Combine(Application.persistentDataPath, "PlayerData.txt");

        [MenuItem("MyTools/ClearAllSaveFiles")]
        private static async void ClearAllSaveFiles()
        {
            if (EditorApplication.isPlaying)
            {
                string playerSaveLoadDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(GetInitialSaveLoadData());
                await CloudSaveService.Instance.Data.Player.SaveAsync(
                    new Dictionary<string, object>
                    {
                        {REMOTE_SAVE_FILE_KEY, playerSaveLoadDataJson}
                    });
                await File.WriteAllTextAsync(SaveFilePath, playerSaveLoadDataJson);
            }
            else 
                Debug.LogError("Log in to the play mod before clearing the data!");
        }

        private static PlayerSaveLoadData GetInitialSaveLoadData() => 
            new() { BestRecord = 0, AdsIsOff = false, DataTime = DateTime.Now };
    }
}
#endif