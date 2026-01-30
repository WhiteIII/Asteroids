using _Project.Scripts.Data.Base;

namespace _Project.Scripts.Data.Services.Repositories.Base
{
    public interface IDataRepository
    {
        T GetData<T>() where T : class, IData;
    }
}
