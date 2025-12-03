using _Project.Scripts.Common.Services.Analytics.Base;
using _Project.Scripts.Common.Services.Analytics.Implementation.Data;
using _Project.Scripts.Gameplay.Characters;
using _Project.Scripts.Gameplay.Characters.Implementation;
using _Project.Scripts.Gameplay.GameProgress;

namespace _Project.Scripts.Gameplay.Services.AnalyticEmplementation
{
    public class StartGameAndEndGameEventSender
    {
        private readonly IEventSender _eventSender;
        private readonly IPointsAndKillsCounterCounter _pointsAndKillsCounter;
        private readonly IWeaponsUsageCounter _weaponsUsageCounter;

        public StartGameAndEndGameEventSender(
            IEventSender eventSender,
            IPointsAndKillsCounterCounter pointsAndKillsPointsAndKillsCounter, 
            IWeaponsUsageCounter weaponsUsageCounter)
        {
            _eventSender = eventSender;
            _pointsAndKillsCounter = pointsAndKillsPointsAndKillsCounter;
            _weaponsUsageCounter = weaponsUsageCounter;
        }

        public void SendEndGameEvent() =>
            _eventSender.SendEvent<AnalyticDataOnGameEnd, int>(
                new AnalyticDataOnGameEnd(
                    _weaponsUsageCounter.BaseWeaponUsageCount.CurrentValue,
                    _weaponsUsageCounter.LazerUsageCount.CurrentValue,
                    _pointsAndKillsCounter.GetKills<Asteroid>(),
                    _pointsAndKillsCounter.GetKills<Ufo>()));

        public void SendStartGameEvent() =>
            _eventSender.SendEvent<AnalyticDataOnGameStart>();
    }
}
