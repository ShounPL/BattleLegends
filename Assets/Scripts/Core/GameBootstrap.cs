using UnityEngine;

namespace BattleLegends
{
    public class GameBootstrap : MonoBehaviour
    {
        public void Start()
        {
            if (ProgressionSystem.Instance == null)
            {
                var progressionObject = new GameObject("ProgressionSystem");
                progressionObject.AddComponent<ProgressionSystem>();
            }

            if (BattleManager.Instance == null)
            {
                var battleObject = new GameObject("BattleManager");
                battleObject.AddComponent<BattleManager>();
            }

            if (FindObjectOfType<MainMenuController>() == null)
            {
                var menuObject = new GameObject("MainMenu");
                menuObject.AddComponent<MainMenuController>();
            }

            if (FindObjectOfType<BattleHUD>() == null)
            {
                var hudObject = new GameObject("BattleHUD");
                hudObject.AddComponent<BattleHUD>();
            }
        }
    }
}
