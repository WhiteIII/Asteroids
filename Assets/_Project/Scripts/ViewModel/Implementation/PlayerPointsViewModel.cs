using _Project.Scripts.Gameplay.Ship;
using R3;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class PlayerPointsViewModel : IViewModel
    {
        public readonly Observable<int> OnPointsChanged;

        public PlayerPointsViewModel(IPointsCounter pointsCounter) => 
            OnPointsChanged = pointsCounter.Points;
    }
}