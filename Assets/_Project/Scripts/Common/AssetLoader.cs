using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Common
{
    public class AssetLoader
    {
        private readonly LocalAssetProvider _localAssetProvider;
        private readonly AssetReference[] _assetsReference;

        public AssetLoader(LocalAssetProvider localAssetProvider, AssetReference[] assetsReference)
        {
            _localAssetProvider = localAssetProvider;
            _assetsReference = assetsReference;
        }

        public async UniTask LoadAssetsAsync()
        {
            foreach (AssetReference assetReference in _assetsReference)
                await _localAssetProvider.LoadAsync(assetReference);
        }
    }
}