using UnityEngine;

namespace BattleLegends
{
    public class DailyRewardScreenController : MonoBehaviour
    {
        private const int PanelWidth = 500;
        private const int PanelHeight = 260;

        private void OnGUI()
        {
            Rect panel = new Rect((Screen.width - PanelWidth) * 0.5f, (Screen.height - PanelHeight) * 0.5f, PanelWidth, PanelHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Nagroda dzienna", GUILayout.Height(36f));

            if (DailyRewardSystem.Instance == null)
            {
                GUILayout.Label("Brak systemu nagród dziennych.");
                GUILayout.EndVertical();
                GUILayout.EndArea();
                return;
            }

           GUILayout.Label("Dziś: +100 zł +25 gemów", GUILayout.Height(24f));
            GUILayout.Label("Streak: " + DailyRewardSystem.Instance.state.streak, GUILayout.Height(24f));
            GUILayout.Label("Ostatnia data: " + DailyRewardSystem.Instance.state.lastClaimDate, GUILayout.Height(24f));

            if (DailyRewardSystem.Instance.CanClaimReward())
            {
                if (GUILayout.Button("Odbierz nagrodę", GUILayout.Height(42f)))
                {
                    DailyRewardSystem.Instance.ClaimReward();
                }
            }
            else
            {
                GUILayout.Label("Nagroda już została odebrana dziś.", GUILayout.Height(42f));
            }

            if (GUILayout.Button("Powrót", GUILayout.Height(36f)))
            {
                SceneLoader.LoadScene("MainMenu");
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
