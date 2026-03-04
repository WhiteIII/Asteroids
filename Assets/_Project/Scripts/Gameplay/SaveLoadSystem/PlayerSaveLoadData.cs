using System;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    [Serializable]
    public class PlayerSaveLoadData
    {
        public int BestRecord { get; set; }
        public bool AdsIsOff { get; set; }
        public DateTime DataTime { get; set; }
    }
}