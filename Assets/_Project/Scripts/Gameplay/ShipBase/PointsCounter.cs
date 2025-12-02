using R3;

namespace _Project.Scripts.Gameplay.ShipBase
{
    public class PointsCounter : IPointsCounter
    {
        public ReactiveProperty<int> Points { get; } = new();

        public void AddPoints(int points) =>
            Points.Value += points;
    }

    public interface IPointsCounter
    {
        ReactiveProperty<int> Points { get; }
    }
}