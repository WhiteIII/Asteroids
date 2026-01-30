using System;
using _Project.Scripts.Data.Base;

namespace _Project.Scripts.Data.Implementation
{
    [Serializable]
    public class AsteroidsConfig : IAsteroidsData
    {
        public int SmallAsteroidsPoints { get; set; }
        public float DirectionDeviationFrom { get; set; }
        public float DirectionDeviationTo { get; set; }
        public float RandomSpeedFrom { get; set; }
        public float RandomSpeedTo { get; set; }
        public int SpawnedSmallAsteroidsOnDeadCountForm { get; set; }
        public int SpawnedSmallAsteroidsOnDeadCountTo { get; set; }
        public int Points { get; set; }

        public AsteroidsConfig SetData(IAsteroidsData asteroidsData)
        {
            SmallAsteroidsPoints = asteroidsData.SmallAsteroidsPoints;
            DirectionDeviationFrom = asteroidsData.DirectionDeviationFrom;
            DirectionDeviationTo = asteroidsData.DirectionDeviationTo;
            RandomSpeedFrom = asteroidsData.RandomSpeedFrom;
            RandomSpeedTo = asteroidsData.RandomSpeedTo;
            SpawnedSmallAsteroidsOnDeadCountForm = asteroidsData.SpawnedSmallAsteroidsOnDeadCountForm;
            SpawnedSmallAsteroidsOnDeadCountTo = asteroidsData.SpawnedSmallAsteroidsOnDeadCountTo;
            Points = asteroidsData.Points;
            return this;
        }
    }
}