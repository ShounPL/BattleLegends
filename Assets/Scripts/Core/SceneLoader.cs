using UnityEngine.SceneManagement;

namespace BattleLegends
{
    public static class SceneLoader
    {
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
