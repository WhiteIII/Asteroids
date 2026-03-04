using _Project.Scripts.Gameplay.GameProgress;
using _Project.Scripts.ViewModel.Base;
using R3;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class PlayerPointsViewModel : IViewModel
    {
        public readonly Observable<int> OnPointsChanged;

        public PlayerPointsViewModel(IPointsAndKillsCounterCounter pointsAndKillsCounterCounter) => 
            OnPointsChanged = pointsAndKillsCounterCounter.Points;
    }
}