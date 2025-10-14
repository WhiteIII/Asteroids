using _Project.Scripts.Data;
using _Project.Scripts.Gameplay.Services.Repositories;
using R3;
using UnityEngine;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class ShipStatsViewModel : IViewModel
    {
        public Observable<float> OnCooldownChanged { get; private set; }
        public Observable<int> OnChargeCountChanged { get; private set; }
        public Observable<Vector3> OnPositionChanged { get; private set; }
        
        private readonly ICharacterRepository _characterRepository; 
        
        public int MaxChargeCount { get; private set; } 
        public float LazerCooldown { get; private set; }
        
        public ShipStatsViewModel(ShipStatsData stats, ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
            MaxChargeCount = stats.LazerChargeCount;
            LazerCooldown = stats.LazerRechargeTime;
        }
        
        public void SetShipObservables()
        {
            OnCooldownChanged = _characterRepository.Ship.OnLazerCooldownChanged;
            OnChargeCountChanged = _characterRepository.Ship.OnLazerChargeCountChanged;
            OnPositionChanged = _characterRepository.Ship.Position;
        }
    }
}