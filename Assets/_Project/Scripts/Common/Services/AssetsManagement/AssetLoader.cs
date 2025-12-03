using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Common.Services.AssetsManagement
{
    public class AssetLoader
    {
        private readonly LocalAssetsProvider _localAssetsProvider;
        private readonly AssetReference[] _assetsReference;

        public AssetLoader(LocalAssetsProvider localAssetsProvider, AssetReference[] assetsReference)
        {
            _localAssetsProvider = localAssetsProvider;
            _assetsReference = assetsReference;
        }

        public UniTask[] GetLoadedAsyncOperations()
        {
            UniTask[] tasks = new UniTask[_assetsReference.Length];
            for (int i = 0; i < _assetsReference.Length; i++)
                tasks[i] = _localAssetsProvider.LoadAsync(_assetsReference[i]);
            return tasks;
        }
    }
}