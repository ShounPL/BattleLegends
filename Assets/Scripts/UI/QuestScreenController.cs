using UnityEngine;

namespace BattleLegends
{
    public class QuestScreenController : MonoBehaviour
    {
        private const int PanelWidth = 900;
        private const int PanelHeight = 500;
        private Vector2 scrollPosition = Vector2.zero;

        private void OnGUI()
        {
            Rect panel = new Rect((Screen.width - PanelWidth) * 0.5f, (Screen.height - PanelHeight) * 0.5f, PanelWidth, PanelHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Misje dzienne i zadania", GUILayout.Height(36f));

            if (QuestSystem.Instance == null)
            {
                GUILayout.Label("Brak systemu questów.");
                GUILayout.EndVertical();
                GUILayout.EndArea();
                return;
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(340f));

            foreach (var quest in QuestSystem.Instance.quests)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label(quest.title, GUILayout.Width(180f));
                GUILayout.Label(quest.description, GUILayout.Width(260f));
                GUILayout.Label(quest.progress + "/" + quest.targetAmount, GUILayout.Width(80f));
                GUILayout.Label(quest.completed ? "Ukończone" : "Aktywne", GUILayout.Width(100f));
                if (quest.completed)
                {
                    GUILayout.Label("Nagroda: " + quest.rewardGold + " zł / " + quest.rewardGems + " gemów", GUILayout.Width(180f));
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
