using UnityEngine;

namespace BattleLegends
{
    public class DungeonRewardTable
    {
        public static DungeonRewardResult RollRewards(DungeonDefinition dungeon)
        {
            var result = new DungeonRewardResult();
            result.gold = dungeon.rewardGold + Random.Range(30, 90);
            result.xp = dungeon.rewardXp + Random.Range(20, 60);

            if (InventorySystem.Instance != null && Random.value < 0.75f)
            {
                var loot = InventorySystem.Instance.AddRandomLoot(dungeon.difficulty);
                result.lootName = loot.itemName;
                result.hasLoot = true;
            }
            else
            {
                result.lootName = "Brak";
                result.hasLoot = false;
            }

            return result;
        }
    }

    public class DungeonRewardResult
    {
        public int gold;
        public int xp;
        public string lootName = "Brak";
        public bool hasLoot;
    }
}
