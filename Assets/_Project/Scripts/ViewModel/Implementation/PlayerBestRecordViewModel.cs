using _Project.Scripts.Gameplay.SaveLoadSystem;
using _Project.Scripts.ViewModel.Base;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class PlayerBestRecordViewModel : IViewModel
    {
        private readonly ISaveLoad _saveLoad;
        
        public int BestRecord => _saveLoad.Load().BestRecord;
        
        public PlayerBestRecordViewModel(ISaveLoad saveLoad) => 
            _saveLoad = saveLoad;
    }
}