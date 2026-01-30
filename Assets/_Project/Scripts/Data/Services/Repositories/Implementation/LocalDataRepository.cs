using _Project.Scripts.Data.Base;
using _Project.Scripts.Data.Services.Repositories.Base;
using UnityEngine;

namespace _Project.Scripts.Data.Services.Repositories.Implementation
{
    [CreateAssetMenu(menuName = "_Project/LocalDataRepository", fileName = "LocalDataRepository")]
    public class LocalDataRepository : ScriptableObject, IDataRepository
    {
        [SerializeField] private ScriptableObject[] _dataList;
        
        public T GetData<T>() 
            where T : class, IData
        {
            foreach (ScriptableObject data in _dataList)
            {
                if (data is T concreteData)
                    return concreteData;
            }
            return null;
        }
    }
}