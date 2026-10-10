using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    [Serializable]
    public class ShopItemDefinition
    {
        public string itemId = "potion_basic";
        public string itemName = "Basic Potion";
        public int cost = 75;
        public ItemRarity rarity = ItemRarity.Common;
        public ItemDefinition itemTemplate;
    }

    public class ShopSystem : MonoBehaviour
    {
        public static ShopSystem Instance { get; private set; }

        public List<ShopItemDefinition> shopItems = new List<ShopItemDefinition>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            CreateDefaultShop();
        }

        private void CreateDefaultShop()
        {
            if (shopItems.Count > 0)
            {
                return;
            }

            var potion = ScriptableObject.CreateInstance<ItemDefinition>();
            potion.itemName = "Health Potion";
            potion.rarity = ItemRarity.Common;
            potion.slot = EquipmentSlot.Accessory;
            potion.healthBonus = 18;
            potion.value = 25;
            potion.description = "Heals 18 HP.";

            var blade = ScriptableObject.CreateInstance<ItemDefinition>();
            blade.itemName = "Iron Fang";
            blade.rarity = ItemRarity.Rare;
            blade.slot = EquipmentSlot.Weapon;
            blade.attackBonus = 8;
            blade.value = 80;
            blade.description = "A sharp blade for warriors.";

            var armor = ScriptableObject.CreateInstance<ItemDefinition>();
            armor.itemName = "Guardian Mail";
            armor.rarity = ItemRarity.Rare;
            armor.slot = EquipmentSlot.Armor;
            armor.defenseBonus = 9;
            armor.value = 90;
            armor.description = "Basic protective armor.";

            shopItems.Add(new ShopItemDefinition { itemId = "potion_basic", itemName = "Basic Potion", cost = 75, rarity = ItemRarity.Common, itemTemplate = potion });
            shopItems.Add(new ShopItemDefinition { itemId = "blade_iron_fang", itemName = "Iron Fang", cost = 240, rarity = ItemRarity.Rare, itemTemplate = blade });
            shopItems.Add(new ShopItemDefinition { itemId = "armor_guardian_mail", itemName = "Guardian Mail", cost = 300, rarity = ItemRarity.Rare, itemTemplate = armor });
        }

        public bool TryBuy(string itemId)
        {
            var shopItem = shopItems.Find(x => x.itemId == itemId);
            if (shopItem == null)
            {
                return false;
            }

            if (ProgressionSystem.Instance == null)
            {
                return false;
            }

            if (ProgressionSystem.Instance.gold < shopItem.cost)
            {
                return false;
            }

            ProgressionSystem.Instance.gold -= shopItem.cost;

            if (InventorySystem.Instance != null && shopItem.itemTemplate != null)
            {
                InventorySystem.Instance.AddItem(shopItem.itemTemplate);
            }

            return true;
        }
    }
}
