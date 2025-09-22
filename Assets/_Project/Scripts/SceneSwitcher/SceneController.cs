using static UnityEngine.SceneManagement.LoadSceneMode;
using static UnityEngine.SceneManagement.SceneManager;

namespace _Project.Scripts.SceneSwitcher
{
    public class SceneController : ISceneController
    {
        private static string GAMEPLAY = "Gameplay";
        private static string MENU = "Menu";
        
        public void GoToMenu()
        {
            if (loadedSceneCount > 1)
                UnloadSceneAsync(GAMEPLAY);
            
            LoadSceneAsync(MENU, Additive);
        }

        public void GoToGameplay()
        {
            
            if (loadedSceneCount > 1)
                UnloadSceneAsync(MENU);
            
            LoadSceneAsync(GAMEPLAY, Additive);
        }
    }

    public interface ISceneController
    {
        void GoToMenu();
        void GoToGameplay();
    }
}
