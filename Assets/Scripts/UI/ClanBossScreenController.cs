using UnityEngine;

namespace BattleLegends
{
    public class ClanBossScreenController : MonoBehaviour
    {
        private const int PanelWidth = 700;
        private const int PanelHeight = 420;

        private void OnGUI()
        {
            Rect panel = new Rect((Screen.width - PanelWidth) * 0.5f, (Screen.height - PanelHeight) * 0.5f, PanelWidth, PanelHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Klan: " + (ClanSystem.Instance != null ? ClanSystem.Instance.clan.clanName : "Brak"), GUILayout.Height(30f));
            GUILayout.Label("Poziom klanu: " + (ClanSystem.Instance != null ? ClanSystem.Instance.clan.level : 0), GUILayout.Height(24f));
            GUILayout.Label("Boss HP: " + (ClanSystem.Instance != null ? ClanSystem.Instance.clan.clanBossHealth : 0), GUILayout.Height(24f));
            GUILayout.Label("Boss DMG: " + (ClanSystem.Instance != null ? ClanSystem.Instance.clan.clanBossDamage : 0), GUILayout.Height(24f));

            if (GUILayout.Button("Rozpocznij boss fight", GUILayout.Height(42f)))
            {
                if (ClanBossManager.Instance != null)
                {
                    ClanBossManager.Instance.StartClanBossFight();
                }
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
