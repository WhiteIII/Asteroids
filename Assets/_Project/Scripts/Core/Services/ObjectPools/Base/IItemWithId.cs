using R3;

namespace _Project.Scripts.Core.Services.ObjectPools.Base
{
    public interface IItemWithId<T>
    {
        Subject<T> Release { get; }
        
        void SetID(T id);
    }
}