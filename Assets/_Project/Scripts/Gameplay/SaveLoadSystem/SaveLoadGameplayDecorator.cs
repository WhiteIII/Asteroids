using _Project.Scripts.Gameplay.GameProgress;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public class PlayerBestRecordSaver
    {
        private readonly ISaveLoad _saveLoad;
        private readonly IPointsAndKillsCounterCounter _pointsAndKillsCounterCounter;

        public PlayerBestRecordSaver(
            ISaveLoad saveLoad,
            IPointsAndKillsCounterCounter pointsAndKillsCounterCounter)
        {
            _saveLoad = saveLoad;
            _pointsAndKillsCounterCounter = pointsAndKillsCounterCounter;
        }
        
        public void TrySaveBestRecord()
        {
            PlayerSaveLoadData saveLoadData = _saveLoad.Load();
            if (saveLoadData.BestRecord < _pointsAndKillsCounterCounter.Points.CurrentValue)
            {
                saveLoadData.BestRecord = _pointsAndKillsCounterCounter.Points.CurrentValue;
                _saveLoad.Save(saveLoadData);
            }
        }
    }
}