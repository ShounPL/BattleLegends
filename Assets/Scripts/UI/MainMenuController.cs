using UnityEngine;

namespace BattleLegends
{
    public class MainMenuController : MonoBehaviour
    {
        private void OnGUI()
        {
            Rect panel = new Rect(30f, 30f, 300f, 260f);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("BATTLE LEGENDS", GUILayout.Height(32f));
            GUILayout.Label("Account Lv. " + ProgressionSystem.Instance.accountLevel, GUILayout.Height(24f));
            GUILayout.Label("Gold: " + ProgressionSystem.Instance.gold + "   Gems: " + ProgressionSystem.Instance.gems, GUILayout.Height(24f));

            if (GUILayout.Button("Play Arena"))
            {
                BattleManager.Instance.StartBattle();
            }

            if (GUILayout.Button("Heroes"))
            {
                Debug.Log("Hero roster placeholder");
            }

            if (GUILayout.Button("Dungeons"))
            {
                Debug.Log("Dungeon screen placeholder");
            }

            if (GUILayout.Button("Clans"))
            {
                Debug.Log("Clan screen placeholder");
            }

            if (GUILayout.Button("Quit"))
            {
                Application.Quit();
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
