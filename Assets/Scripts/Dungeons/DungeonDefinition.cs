using UnityEngine;

namespace BattleLegends
{
    [CreateAssetMenu(menuName = "BattleLegends/Dungeon Definition")]
    public class DungeonDefinition : ScriptableObject
    {
        public string dungeonName = "Cursed Grove";
        public int difficulty = 1;
        public int recommendedLevel = 1;
        public int rewardGold = 220;
        public int rewardXp = 90;
        public int rewardGemBonus = 10;
        public string[] enemyNames = new[] { "Goblin Raider", "Bone Archer", "Wisp Fang" };
        public int enemyCount = 3;
        public int baseEnemyHealth = 90;
        public int baseEnemyDamage = 12;
        public float enemySpeed = 2.4f;
        public string bossName = "Root Warden";
        public int bossHealth = 320;
        public int bossDamage = 26;
        public float bossSpeed = 2.0f;
        [TextArea(2, 4)] public string description = "A dangerous dungeon filled with monsters.";
    }
}
