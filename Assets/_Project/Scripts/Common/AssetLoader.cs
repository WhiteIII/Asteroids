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

        public UniTask[] GetLoadedAsyncOperations()
        {
            UniTask[] tasks = new UniTask[_assetsReference.Length];
            for (int i = 0; i < _assetsReference.Length; i++)
                tasks[i] = _localAssetProvider.LoadAsync(_assetsReference[i]);
            return tasks;
        }
    }
}