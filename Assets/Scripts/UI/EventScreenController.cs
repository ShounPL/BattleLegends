using UnityEngine;

namespace BattleLegends
{
    public class EventScreenController : MonoBehaviour
    {
        private const int PanelWidth = 700;
        private const int PanelHeight = 400;

        private void OnGUI()
        {
            Rect panel = new Rect((Screen.width - PanelWidth) * 0.5f, (Screen.height - PanelHeight) * 0.5f, PanelWidth, PanelHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Event Aktywny", GUILayout.Height(36f));

            if (EventSystem.Instance == null || EventSystem.Instance.eventData == null)
            {
                GUILayout.Label("Brak eventu");
                GUILayout.EndVertical();
                GUILayout.EndArea();
                return;
            }

            var eventData = EventSystem.Instance.eventData;
            GUILayout.Label(eventData.eventName, GUILayout.Height(24f));
            GUILayout.Label(eventData.description, GUILayout.Height(40f));
            GUILayout.Label("Postęp: " + eventData.progress + " / " + eventData.target, GUILayout.Height(24f));
            GUILayout.Label("Nagroda: +" + eventData.rewardGold + " zł   +" + eventData.rewardGems + " gemów", GUILayout.Height(24f));
            GUILayout.Label("Koniec eventu: " + eventData.endDate, GUILayout.Height(24f));

            if (EventSystem.Instance.CanClaimReward())
            {
                if (GUILayout.Button("Odbierz nagrodę", GUILayout.Height(42f)))
                {
                    EventSystem.Instance.ClaimReward();
                }
            }
            else if (eventData.claimed)
            {
                GUILayout.Label("Nagroda odebrana", GUILayout.Height(42f));
            }
            else
            {
                GUILayout.Label("Brakuje " + (eventData.target - eventData.progress) + " punktów", GUILayout.Height(42f));
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
