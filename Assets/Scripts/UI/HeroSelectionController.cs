using UnityEngine;

namespace BattleLegends
{
    public class HeroSelectionController : MonoBehaviour
    {
        private const int PanelWidth = 700;
        private const int PanelHeight = 420;

        private readonly string[] names = { "Astra", "Varek", "Lyra" };
        private readonly string[] roles = { "Frontline Guardian", "Shadow Striker", "Arc Mage" };
        private readonly int[] health = { 120, 145, 130 };
        private readonly int[] damage = { 20, 28, 24 };

        private void OnGUI()
        {
            Rect panel = new Rect((Screen.width - PanelWidth) * 0.5f, (Screen.height - PanelHeight) * 0.5f, PanelWidth, PanelHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Wybierz bohatera", GUILayout.Height(36f));

            for (int i = 0; i < names.Length; i++)
            {
                GUILayout.BeginHorizontal("box");

                GUILayout.Label(names[i], GUILayout.Width(100f));
                GUILayout.Label(roles[i], GUILayout.Width(180f));
                GUILayout.Label("HP " + health[i], GUILayout.Width(70f));
                GUILayout.Label("DMG " + damage[i], GUILayout.Width(80f));

                if (BattleManager.Instance != null && BattleManager.Instance.selectedHeroIndex == i)
                {
                    GUILayout.Label("Wybrany", GUILayout.Width(90f));
                }

                if (GUILayout.Button("Wybierz", GUILayout.Width(100f)))
                {
                    if (BattleManager.Instance != null)
                    {
                        BattleManager.Instance.SetSelectedHero(i);
                    }

                    SceneLoader.LoadScene("BattleArena");
                }

                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Powrót do menu", GUILayout.Height(42f)))
            {
                SceneLoader.LoadScene("MainMenu");
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
