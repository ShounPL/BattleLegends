using UnityEngine;

namespace BattleLegends
{
    public class DungeonResultController : MonoBehaviour
    {
        public static DungeonResultController Instance { get; private set; }

        private bool isVisible;
        private bool victory;
        private int goldReward;
        private int xpReward;
        private string lootName = "Brak";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            HideResult();
        }

        public void ShowResult(bool isVictory, int gold, int xp, string loot)
        {
            victory = isVictory;
            goldReward = gold;
            xpReward = xp;
            lootName = loot;
            isVisible = true;
        }

        public void HideResult()
        {
            isVisible = false;
            victory = false;
            goldReward = 0;
            xpReward = 0;
            lootName = "Brak";
        }

        private void OnGUI()
        {
            if (!isVisible)
            {
                return;
            }

            Rect panel = new Rect((Screen.width - 420f) * 0.5f, (Screen.height - 260f) * 0.5f, 420f, 260f);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label(victory ? "Loch wyczyszczony!" : "Loch pokonany...", GUILayout.Height(36f));
            GUILayout.Label("Złoto: " + goldReward, GUILayout.Height(28f));
            GUILayout.Label("XP: " + xpReward, GUILayout.Height(28f));
            GUILayout.Label("Loot: " + lootName, GUILayout.Height(28f));

            if (GUILayout.Button("Powrót do menu", GUILayout.Height(42f)))
            {
                HideResult();
                SceneLoader.LoadScene("MainMenu");
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
