using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static UnityEngine.AddressableAssets.Addressables;

namespace _Project.Scripts.Common
{
    public class LocalAssetProvider
    {
        public async UniTask<T> LoadAsync<T>(string id) where T : MonoBehaviour
        {
             GameObject createdObject = await InstantiateAsync(id).Task;
            
             if (createdObject.TryGetComponent(out T result))
                 return result;
             throw new Exception("Component not found on loaded asset!");
        }
        
        public void Unload<T>(T loadedObject) where T : MonoBehaviour => 
            ReleaseInstance(loadedObject.gameObject);
    }
}