using R3;

namespace _Project.Scripts.Gameplay.GameProgress
{
    public class WeaponsUsageCounter : IWeaponsUsageCounter
    {
        public ReadOnlyReactiveProperty<int> LazerUsageCount => _lazerUsageCount;
        public ReadOnlyReactiveProperty<int> BaseWeaponUsageCount => _baseWeaponUsageCount;

        private readonly ReactiveProperty<int> _lazerUsageCount = new();
        private readonly ReactiveProperty<int> _baseWeaponUsageCount = new();
        
        public void AddLazerUsageCount() => 
            _lazerUsageCount.Value++;
        
        public void AddBaseWeaponUsageCount() =>
            _baseWeaponUsageCount.Value++;
    }

    public interface IWeaponsUsageCounter
    {
        ReadOnlyReactiveProperty<int> LazerUsageCount { get; }
        ReadOnlyReactiveProperty<int> BaseWeaponUsageCount { get; }
    }
}