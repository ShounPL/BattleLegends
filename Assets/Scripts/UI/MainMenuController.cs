using UnityEngine;

namespace BattleLegends
{
    public class MainMenuController : MonoBehaviour
    {
        private const int MenuWidth = 340;
        private const int MenuHeight = 360;

        private void OnGUI()
        {
            var progression = ProgressionSystem.Instance;
            if (progression == null)
            {
                return;
            }

            Rect panel = new Rect((Screen.width - MenuWidth) * 0.5f, (Screen.height - MenuHeight) * 0.5f, MenuWidth, MenuHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("BATTLE LEGENDS", GUILayout.Height(32f));
            GUILayout.Label("Account Lv. " + progression.accountLevel + "   Hero Lv. " + progression.heroLevel, GUILayout.Height(24f));
            GUILayout.Label("Gold: " + progression.gold + "   Gems: " + progression.gems, GUILayout.Height(24f));
            GUILayout.Label("XP: " + progression.xp + " / " + progression.requiredXp, GUILayout.Height(24f));

            if (GUILayout.Button("Play Arena", GUILayout.Height(42f)))
            {
                if (BattleManager.Instance != null)
                {
                    BattleManager.Instance.StartBattle();
                }
            }

            if (GUILayout.Button("Heroes", GUILayout.Height(42f)))
            {
                Debug.Log("Hero roster opened");
            }

            if (GUILayout.Button("Dungeons", GUILayout.Height(42f)))
            {
                Debug.Log("Dungeon screen placeholder");
            }

            if (GUILayout.Button("Clans", GUILayout.Height(42f)))
            {
                Debug.Log("Clan screen placeholder");
            }

            if (GUILayout.Button("Quit", GUILayout.Height(42f)))
            {
                Application.Quit();
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
