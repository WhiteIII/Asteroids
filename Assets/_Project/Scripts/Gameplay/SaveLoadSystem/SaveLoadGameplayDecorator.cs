using _Project.Scripts.Gameplay.GameProgress;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class PlayerBestRecordSaver
    {
        private readonly ISaveLoadAsync _saveLoad;
        private readonly IPointsAndKillsCounterCounter _pointsAndKillsCounterCounter;

        public PlayerBestRecordSaver(
            ISaveLoadAsync saveLoad,
            IPointsAndKillsCounterCounter pointsAndKillsCounterCounter)
        {
            _saveLoad = saveLoad;
            _pointsAndKillsCounterCounter = pointsAndKillsCounterCounter;
        }
        
        public async UniTask TrySaveBestRecord()
        {
            PlayerSaveLoadData saveLoadData = await _saveLoad.LoadAsync();
            if (saveLoadData.BestRecord < _pointsAndKillsCounterCounter.Points.CurrentValue)
            {
                saveLoadData.BestRecord = _pointsAndKillsCounterCounter.Points.CurrentValue;
                await _saveLoad.SaveAsync(saveLoadData);
            }
        }
    }
}