using UnityEngine;

namespace BattleLegends
{
    public class MobileTouchControls : MonoBehaviour
    {
        private Rect leftPad;
        private Rect rightPad;

        private void Start()
        {
            leftPad = new Rect(20f, Screen.height - 180f, 150f, 150f);
            rightPad = new Rect(Screen.width - 170f, Screen.height - 180f, 150f, 150f);
        }

        private void OnGUI()
        {
            if (!BattleManager.Instance || !BattleManager.Instance.battleActive)
            {
                return;
            }

            GUI.Box(leftPad, "Move");
            GUI.Box(rightPad, "Skills");

            if (GUI.Button(new Rect(rightPad.x + 20f, rightPad.y + 35f, 110f, 42f), "Q"))
            {
                BattleManager.Instance.HeroCast(0, 0);
            }

            if (GUI.Button(new Rect(rightPad.x + 20f, rightPad.y + 90f, 110f, 42f), "E"))
            {
                BattleManager.Instance.HeroCast(0, 1);
            }
        }
    }
}
