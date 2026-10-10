using UnityEngine;

namespace BattleLegends
{
    public class BattleHUD : MonoBehaviour
    {
        private void OnGUI()
        {
            if (!BattleManager.Instance || !BattleManager.Instance.battleActive)
            {
                return;
            }

            GUI.Box(new Rect(10f, 10f, 220f, 80f), "Heroes");
            int y = 30;
            foreach (var hero in BattleManager.Instance.heroes)
            {
                if (hero == null)
                {
                    continue;
                }

                GUI.Label(new Rect(20f, y, 180f, 20f), hero.actorName + " HP: " + Mathf.RoundToInt(hero.health));
                y += 20;
            }

            GUI.Box(new Rect(Screen.width - 230f, 10f, 220f, 80f), "Enemies");
            int enemyY = 30;
            foreach (var enemy in BattleManager.Instance.enemies)
            {
                if (enemy == null)
                {
                    continue;
                }

                GUI.Label(new Rect(Screen.width - 220f, enemyY, 180f, 20f), enemy.actorName + " HP: " + Mathf.RoundToInt(enemy.health));
                enemyY += 20;
            }

            if (GUI.Button(new Rect(Screen.width * 0.5f - 80f, Screen.height - 60f, 160f, 40f), "Use Skill Q"))
            {
                BattleManager.Instance.HeroCast(0, 0);
            }

            if (GUI.Button(new Rect(Screen.width * 0.5f - 80f, Screen.height - 105f, 160f, 40f), "Use Skill E"))
            {
                BattleManager.Instance.HeroCast(0, 1);
            }
        }
    }
}
