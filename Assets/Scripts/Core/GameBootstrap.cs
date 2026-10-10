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
                var menu = new GameObject("MainMenu");
                menu.AddComponent<MainMenuController>();
            }

            if (FindObjectOfType<BattleHUD>() == null)
            {
                var hud = new GameObject("BattleHUD");
                hud.AddComponent<BattleHUD>();
            }

            if (FindObjectOfType<MobileTouchControls>() == null)
            {
                var touch = new GameObject("MobileTouchControls");
                touch.AddComponent<MobileTouchControls>();
            }

            if (FindObjectOfType<BattleResultController>() == null)
            {
                var result = new GameObject("BattleResult");
                result.AddComponent<BattleResultController>();
            }
        }
    }
}
