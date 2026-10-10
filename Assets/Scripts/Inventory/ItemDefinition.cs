using UnityEngine;

namespace BattleLegends
{
    [CreateAssetMenu(menuName = "BattleLegends/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        public string itemName = "Iron Blade";
        public ItemRarity rarity = ItemRarity.Common;
        public EquipmentSlot slot = EquipmentSlot.Weapon;
        public int attackBonus = 0;
        public int defenseBonus = 0;
        public int healthBonus = 0;
        public int value = 25;
        public string description = "A basic equipment item.";
    }

    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public enum EquipmentSlot
    {
        Weapon,
        Armor,
        Accessory,
        Relic
    }
}
