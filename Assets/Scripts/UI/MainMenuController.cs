using UnityEngine;

namespace BattleLegends
{
    public class MainMenuController : MonoBehaviour
    {
        private const int MenuWidth = 470;
        private const int MenuHeight = 620;

        private void OnGUI()
        {
            var progression = ProgressionSystem.Instance;
            Rect panel = new Rect((Screen.width - MenuWidth) * 0.5f, (Screen.height - MenuHeight) * 0.5f, MenuWidth, MenuHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("BATTLE LEGENDS", GUILayout.Height(32f));
            GUILayout.Label("Account Lv. " + (progression != null ? progression.accountLevel : 1), GUILayout.Height(24f));
            GUILayout.Label("Gold: " + (progression != null ? progression.gold : 0) + "   Gems: " + (progression != null ? progression.gems : 0), GUILayout.Height(24f));
            GUILayout.Label("XP: " + (progression != null ? progression.xp : 0) + " / " + (progression != null ? progression.requiredXp : 100), GUILayout.Height(24f));

            if (GUILayout.Button("Play Arena", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("HeroSelect");
            }

            if (GUILayout.Button("PvP Arena", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("ArenaSelection");
            }

            if (GUILayout.Button("Ranking", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("Leaderboard");
            }

            if (GUILayout.Button("Heroes", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("HeroRoster");
            }

            if (GUILayout.Button("Dungeons", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("DungeonSelection");
            }

            if (GUILayout.Button("Inventory", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("Inventory");
            }

            if (GUILayout.Button("Shop", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("Shop");
            }

            if (GUILayout.Button("Quests", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("Quests");
            }

            if (GUILayout.Button("Daily Reward", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("DailyReward");
            }

            if (GUILayout.Button("Clan Boss", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("ClanBoss");
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
