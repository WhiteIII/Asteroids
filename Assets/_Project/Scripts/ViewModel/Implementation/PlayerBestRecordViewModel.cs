using _Project.Scripts.Gameplay.SaveLoadSystem;
using _Project.Scripts.ViewModel.Base;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class PlayerBestRecordViewModel : IViewModel, IInitializable
    {
        private readonly ISaveLoadAsync _saveLoad;

        public int BestRecord { get; private set; }

        public PlayerBestRecordViewModel(ISaveLoadAsync saveLoad) => 
            _saveLoad = saveLoad;

        public void Initialize() => 
            GetBestRecordAsync().Forget();

        public async UniTask GetBestRecordAsync()
        {
            PlayerSaveLoadData playerSaveLoadData = await _saveLoad.LoadAsync();
            BestRecord = playerSaveLoadData.BestRecord;
        }
    }
}