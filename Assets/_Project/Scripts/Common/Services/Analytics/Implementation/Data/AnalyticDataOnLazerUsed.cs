using _Project.Scripts.Common.Services.Analytics.Base.Data;

namespace _Project.Scripts.Common.Services.Analytics.Implementation.Data
{
    public struct AnalyticDataOnLazerUsed : IAnalyticData
    {
        private const string ON_LAZER_SHOT_NAME = "on_lazer_shot";
        public string ID => ON_LAZER_SHOT_NAME;
    }
}