using System.Collections.Generic;
using _Project.Scripts.Common.Services.Analytics.Base.Data;

namespace _Project.Scripts.Common.Services.Analytics.Implementation.Data
{
    public struct AnalyticDataOnGameEnd : IAnalyticData<int>
    {
        private const string NAME = "game_ended";
        private const string NUMBER_OF_SHOTS_NAME = "number_of_shots";
        private const string NUMBER_OF_LAZER_SHOTS_NAME = "number_of_lazer_shots";
        private const string DESTROYED_ASTEROIDS_COUNT = "destroyed_asteroids_count";
        private const string DESTROYED_UFO_COUNT = "destroyed_ufo_count";
        
        public string ID => NAME;
        public IReadOnlyDictionary<string, int> Data { get; }

        public AnalyticDataOnGameEnd(
            int numberOfShots, 
            int numberOfLazerShots, 
            int destroyedAsteroidsCount, 
            int destroyedUfoCount)
        {
            Data = new Dictionary<string, int>
            {
                { NUMBER_OF_SHOTS_NAME, numberOfShots },
                { NUMBER_OF_LAZER_SHOTS_NAME, numberOfLazerShots },
                { DESTROYED_ASTEROIDS_COUNT, destroyedAsteroidsCount },
                { DESTROYED_UFO_COUNT, destroyedUfoCount }
            };
        }
    }
}
