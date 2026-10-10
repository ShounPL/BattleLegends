using UnityEngine;

namespace BattleLegends
{
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectOfType<ProgressionSystem>() == null)
            {
                var progression = new GameObject("ProgressionSystem");
                progression.AddComponent<ProgressionSystem>();
            }

            if (FindObjectOfType<BattleManager>() == null)
            {
                var battle = new GameObject("BattleManager");
                battle.AddComponent<BattleManager>();
            }

            if (FindObjectOfType<MainMenuController>() == null)
            {
                var menu = new GameObject("MainMenuController");
                menu.AddComponent<MainMenuController>();
            }
        }

        private void Start()
        {
            SceneLoader.LoadScene("MainMenu");
        }
    }
}
