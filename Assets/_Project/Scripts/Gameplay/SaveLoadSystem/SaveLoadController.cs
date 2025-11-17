using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class SaveLoadController : MonoBehaviour, IInitializable
    {
        //private LevelProgress _levelProgress;
        private PlayerSaveLoadData _currentSaveData;

        [Inject] private SaveLoad _saveLoad;
        
        [Inject] private void Construct(SaveLoad saveLoad) => 
            _saveLoad = saveLoad;
        
        public void Initialize()
        {
            _currentSaveData = _saveLoad.Load();

            //_levelProgress.SetCurrentLevelIndex(_currentSaveData.CurrentLevel);
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void OnDestroy()
        {
            Save();
        }

        public void Save()
        {
            //_currentSaveData.CurrentLevel = _levelProgress.GetCurrentLevelIndex();
            _saveLoad.Save(_currentSaveData);
        }
    }
}