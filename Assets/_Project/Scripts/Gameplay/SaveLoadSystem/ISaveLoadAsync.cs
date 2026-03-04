using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public interface ISaveLoadAsync
    {
        UniTask<PlayerSaveLoadData> LoadAsync();
        UniTask SaveAsync(PlayerSaveLoadData data);
    }
}