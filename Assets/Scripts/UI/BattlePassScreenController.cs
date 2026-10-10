using UnityEngine;

namespace BattleLegends
{
    public class BattlePassScreenController : MonoBehaviour
    {
        private const int PanelWidth = 900;
        private const int PanelHeight = 520;
        private Vector2 scrollPosition = Vector2.zero;

        private void OnGUI()
        {
            Rect panel = new Rect((Screen.width - PanelWidth) * 0.5f, (Screen.height - PanelHeight) * 0.5f, PanelWidth, PanelHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Battle Pass", GUILayout.Height(36f));

            if (BattlePassSystem.Instance == null || BattlePassSystem.Instance.data == null)
            {
                GUILayout.Label("Brak systemu Battle Pass");
                GUILayout.EndVertical();
                GUILayout.EndArea();
                return;
            }

            GUILayout.Label("Poziom: " + BattlePassSystem.Instance.data.level + "    XP: " + BattlePassSystem.Instance.data.xp + " / " + BattlePassSystem.Instance.data.xpToNext, GUILayout.Height(24f));
            GUILayout.Label("Postęp: " + Mathf.RoundToInt((float)BattlePassSystem.Instance.data.xp / BattlePassSystem.Instance.data.xpToNext * 100f) + "%", GUILayout.Height(24f));

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(340f));

            foreach (var reward in BattlePassSystem.Instance.data.rewards)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label("Tier " + reward.tier, GUILayout.Width(70f));
                GUILayout.Label(reward.rewardTitle, GUILayout.Width(160f));
                GUILayout.Label("Gold: +" + reward.goldReward + "  Gems: +" + reward.gemReward, GUILayout.Width(180f));

                if (reward.claimed)
                {
                    GUILayout.Label("Odebrane", GUILayout.Width(100f));
                }
                else if (reward.tier <= BattlePassSystem.Instance.data.level)
                {
                    if (GUILayout.Button("Odbierz", GUILayout.Width(100f)))
                    {
                        BattlePassSystem.Instance.ClaimReward(reward.tier);
                    }
                }
                else
                {
                    GUILayout.Label("Zamknij poziom", GUILayout.Width(120f));
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            if (GUILayout.Button("Powrót", GUILayout.Height(36f)))
            {
                SceneLoader.LoadScene("MainMenu");
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
