using _Project.Scripts.Data.Implementation;
using _Project.Scripts.Data.Services.Repositories.Base;
using _Project.Scripts.Gameplay.Services.Repositories;
using _Project.Scripts.ViewModel.Base;
using R3;
using UnityEngine;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class ShipStatsViewModel : IViewModel
    {
        public Observable<float> OnCooldownChanged { get; private set; }
        public Observable<int> OnChargeCountChanged { get; private set; }
        public Observable<Vector3> OnPositionChanged { get; private set; }
        public Observable<Vector3> OnRotationChanged { get; private set; }
        public Observable<float> OnVelocityChanged { get; private set; }
        
        private readonly ICharacterRepository _characterRepository; 
        
        public int MaxChargeCount { get; private set; } 
        public float LazerCooldown { get; private set; }
        
        public ShipStatsViewModel(IDataRepository dataRepository, ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
            ShipStatsConfig stats = dataRepository.GetData<ShipStatsConfig>();
            MaxChargeCount = stats.LazerChargeCount;
            LazerCooldown = stats.LazerRechargeTime;
        }
        
        public void SetShipObservables()
        {
            OnCooldownChanged = _characterRepository.Ship.OnLazerCooldownChanged;
            OnChargeCountChanged = _characterRepository.Ship.OnLazerChargeCountChanged;
            OnPositionChanged = _characterRepository.Ship.Position;
            OnRotationChanged = _characterRepository.Ship.Rotation;
            OnVelocityChanged = _characterRepository.Ship.Acceleration;
        }
    }
}