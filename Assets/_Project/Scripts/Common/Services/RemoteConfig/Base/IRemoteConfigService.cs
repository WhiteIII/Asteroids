using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Common.Services.RemoteConfig.Base
{
    public interface IRemoteConfigService
    {
        UniTask FetchAsync();
        UniTask ActivateAsync();
        T GetConfig<T>(string key);
    }
}
