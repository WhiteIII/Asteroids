using _Project.Scripts.Gameplay.SaveLoadSystem;
using _Project.Scripts.ViewModel.Base;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class PlayerBestRecordViewModel : IViewModel
    {
        private readonly SaveLoad _saveLoad;
        
        public int BestRecord => _saveLoad.Load().BestRecord;
        
        public PlayerBestRecordViewModel(SaveLoad saveLoad) => 
            _saveLoad = saveLoad;
    }
}