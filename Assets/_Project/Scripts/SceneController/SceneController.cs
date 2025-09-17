using static UnityEngine.SceneManagement.LoadSceneMode;
using static UnityEngine.SceneManagement.SceneManager;

namespace _Project.Scripts.SceneController
{
    public class ScenesController : IScenesController
    {
        private const string GAME = "GameplayScene";
        private const string MENU = "MenuScene";

        public void GoToGame()
        {
            if (loadedSceneCount > 1)
                UnloadSceneAsync(MENU);

            LoadScene(GAME, Additive);
        }

        public void GoToMenu()
        {
            if (loadedSceneCount > 1)
                UnloadSceneAsync(GAME);
            
            LoadScene(MENU, Additive);
        }
    }

    public interface IScenesController
    {
        void GoToGame();
        void GoToMenu();
    }
}
