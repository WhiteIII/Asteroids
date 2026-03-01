using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Common.Services.AssetsManagement
{
    public class AssetLoader
    {
        private readonly LocalAssetsProvider _localAssetsProvider;
        private readonly List<AssetReference> _assetsReference = new();
        
        public AssetLoader(LocalAssetsProvider localAssetsProvider, AssetReference[] assetsReference)
        {
            _localAssetsProvider = localAssetsProvider;
            _assetsReference.AddRange(assetsReference);
        }

        public void AddAsset(AssetReference assetReference) =>  
            _assetsReference.Add(assetReference);
        
        public UniTask[] GetLoadedAsyncOperations()
        {
            UniTask[] tasks = new UniTask[_assetsReference.Count];
            for (int i = 0; i < _assetsReference.Count; i++)
                tasks[i] = _localAssetsProvider.LoadAsync(_assetsReference[i]);
            return tasks;
        }

        public void ReleaseAllLoadedAssets()
        {
            foreach (AssetReference assetReference in _assetsReference)
                _localAssetsProvider.ReleaseAsset(assetReference);
        }
    }
}