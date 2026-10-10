using UnityEngine;

namespace BattleLegends
{
    public class MainMenuController : MonoBehaviour
    {
        private const int MenuWidth = 360;
        private const int MenuHeight = 390;

        private enum MenuState
        {
            Main,
            HeroSelect
        }

        private MenuState currentState = MenuState.Main;
        private readonly string[] heroNames = { "Astra", "Varek", "Lyra" };
        private readonly string[] heroRoles = { "Frontline Guardian", "Shadow Striker", "Arc Mage" };
        private readonly int[] heroHealth = { 120, 145, 130 };
        private readonly int[] heroDamage = { 20, 28, 24 };

        private void OnGUI()
        {
            if (currentState == MenuState.HeroSelect)
            {
                DrawHeroSelectScreen();
                return;
            }

            DrawMainMenu();
        }

        private void DrawMainMenu()
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
                currentState = MenuState.HeroSelect;
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

        private void DrawHeroSelectScreen()
        {
            Rect panel = new Rect((Screen.width - 700f) * 0.5f, (Screen.height - 420f) * 0.5f, 700f, 420f);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Select Your Hero", GUILayout.Height(36f));

            for (int i = 0; i < heroNames.Length; i++)
            {
                bool isSelected = BattleManager.Instance != null && BattleManager.Instance.selectedHeroIndex == i;
                GUILayout.BeginHorizontal("box");
                GUILayout.Label(heroNames[i], GUILayout.Width(100f));
                GUILayout.Label(heroRoles[i], GUILayout.Width(180f));
                GUILayout.Label("HP " + heroHealth[i], GUILayout.Width(70f));
                GUILayout.Label("DMG " + heroDamage[i], GUILayout.Width(80f));

                if (isSelected)
                {
                    GUILayout.Label("Selected", GUILayout.Width(80f));
                }

                if (GUILayout.Button("Choose", GUILayout.Width(90f)))
                {
                    if (BattleManager.Instance != null)
                    {
                        BattleManager.Instance.SetSelectedHero(i);
                    }
                    currentState = MenuState.Main;
                }

                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Back", GUILayout.Height(42f)))
            {
                currentState = MenuState.Main;
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
