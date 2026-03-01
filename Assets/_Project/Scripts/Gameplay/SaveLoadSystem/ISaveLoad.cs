namespace _Project.Scripts.Gameplay.SaveLoadSystem
{
    public interface ISaveLoad
    {
        PlayerSaveLoadData Load();
        void Save(PlayerSaveLoadData data);
    }
}