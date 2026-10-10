using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    public class DungeonSelectionController : MonoBehaviour
    {
        private const int PanelWidth = 700;
        private const int PanelHeight = 420;

        private readonly List<DungeonDefinition> dungeons = new List<DungeonDefinition>();

        private void Start()
        {
            if (dungeons.Count == 0)
            {
                CreateDefaultDungeons();
            }
        }

        private void CreateDefaultDungeons()
        {
            dungeons.Add(CreateDungeon("Cursed Grove", 1, 1, 220, 90, "Root Warden", 320, 26));
            dungeons.Add(CreateDungeon("Sunken Catacombs", 2, 5, 360, 140, "Tide Reaper", 420, 34));
            dungeons.Add(CreateDungeon("Molten Rift", 3, 10, 540, 200, "Ash Titan", 580, 44));
        }

        private DungeonDefinition CreateDungeon(string name, int difficulty, int level, int gold, int xp, string bossName, int bossHealth, int bossDamage)
        {
            var dungeon = ScriptableObject.CreateInstance<DungeonDefinition>();
            dungeon.dungeonName = name;
            dungeon.difficulty = difficulty;
            dungeon.recommendedLevel = level;
            dungeon.rewardGold = gold;
            dungeon.rewardXp = xp;
            dungeon.rewardGemBonus = 10 + difficulty * 5;
            dungeon.enemyNames = new[] { "Goblin Raider", "Bone Archer", "Wisp Fang", "Shade Hunter" };
            dungeon.enemyCount = 3 + difficulty;
            dungeon.baseEnemyHealth = 90 + difficulty * 35;
            dungeon.baseEnemyDamage = 12 + difficulty * 5;
            dungeon.enemySpeed = 2.4f + difficulty * 0.2f;
            dungeon.bossName = bossName;
            dungeon.bossHealth = bossHealth;
            dungeon.bossDamage = bossDamage;
            dungeon.bossSpeed = 2.0f;
            dungeon.description = "Dangerous dungeon with a strong boss and rare loot.";
            return dungeon;
        }

        private void OnGUI()
        {
            Rect panel = new Rect((Screen.width - PanelWidth) * 0.5f, (Screen.height - PanelHeight) * 0.5f, PanelWidth, PanelHeight);
            GUILayout.BeginArea(panel);
            GUILayout.BeginVertical("box");

            GUILayout.Label("Wybór lochu", GUILayout.Height(36f));

            for (int i = 0; i < dungeons.Count; i++)
            {
                var dungeon = dungeons[i];
                GUILayout.BeginHorizontal("box");

                GUILayout.Label(dungeon.dungeonName, GUILayout.Width(180f));
                GUILayout.Label("Lvl " + dungeon.recommendedLevel, GUILayout.Width(80f));
                GUILayout.Label("Trudność " + dungeon.difficulty, GUILayout.Width(90f));
                GUILayout.Label("Nagroda " + dungeon.rewardGold + " zł", GUILayout.Width(130f));

                if (GUILayout.Button("Wejdź", GUILayout.Width(90f)))
                {
                    if (DungeonManager.Instance != null)
                    {
                        DungeonManager.Instance.StartDungeon(dungeon);
                    }
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
