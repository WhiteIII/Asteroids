using _Project.Scripts.Common.Services.Analytics.Base.Data;

namespace _Project.Scripts.Common.Services.Analytics.Implementation.Data
{
    public struct AnalyticDataOnGameStart : IAnalyticData
    {
        private const string GAME_START_NAME = "game_start";
        
        public string ID => GAME_START_NAME;
    }
}