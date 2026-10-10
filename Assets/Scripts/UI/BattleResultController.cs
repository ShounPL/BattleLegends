using UnityEngine;

namespace BattleLegends
{
    public class BattleResultController : MonoBehaviour
    {
        public static BattleResultController Instance { get; private set; }

        private bool isVisible;
        private bool isVictory;
        private int goldReward;
        private int xpReward;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            HideResult();
        }

        public void ShowResult(bool victory, int gold, int xp)
        {
            isVisible = true;
            isVictory = victory;
            goldReward = gold;
            xpReward = xp;
        }

        public void HideResult()
        {
            isVisible = false;
            isVictory = false;
            goldReward = 0;
            xpReward = 0;
        }

        private void OnGUI()
        {
            if (!isVisible)
            {
                return;
            }

            Rect panel = new Rect((Screen.width - 420f) * 0.5f, (Screen.height - 280f) * 0.5f, 420f, 280f);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label(isVictory ? "Victory!" : "Defeat", GUILayout.Height(40f));
            GUILayout.Label("Gold Earned: " + goldReward, GUILayout.Height(28f));
            GUILayout.Label("XP Earned: " + xpReward, GUILayout.Height(28f));

            if (GUILayout.Button("Continue", GUILayout.Height(40f)))
            {
                if (BattleManager.Instance != null)
                {
                    BattleManager.Instance.ReturnToMenu();
                }
            }

            if (GUILayout.Button("Rematch", GUILayout.Height(40f)))
            {
                if (BattleManager.Instance != null)
                {
                    BattleManager.Instance.StartBattle();
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
