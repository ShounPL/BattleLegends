using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    [Serializable]
    public class InventoryItem
    {
        public string itemId = "";
        public string itemName = "";
        public ItemRarity rarity = ItemRarity.Common;
        public EquipmentSlot slot = EquipmentSlot.Weapon;
        public int attackBonus = 0;
        public int defenseBonus = 0;
        public int healthBonus = 0;
        public int value = 0;
        public string description = "";
        public bool equipped = false;
    }

    [Serializable]
    public class InventorySaveData
    {
        public List<InventoryItem> items = new List<InventoryItem>();
    }

    public class InventorySystem : MonoBehaviour
    {
        public static InventorySystem Instance { get; private set; }

        public List<InventoryItem> items = new List<InventoryItem>();
        private readonly Dictionary<EquipmentSlot, InventoryItem> equippedLookup = new Dictionary<EquipmentSlot, InventoryItem>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadInventory();
        }

        private void OnDestroy()
        {
            SaveInventory();
        }

        public void LoadInventory()
        {
            var progress = SaveSystem.Load();
            if (string.IsNullOrEmpty(progress.inventoryJson))
            {
                return;
            }

            var loaded = JsonUtility.FromJson<InventorySaveData>(progress.inventoryJson);
            if (loaded == null || loaded.items == null)
            {
                return;
            }

            items = loaded.items;
            RebuildEquippedLookup();
        }

        public void SaveInventory()
        {
            if (ProgressionSystem.Instance == null)
            {
                return;
            }

            var saveData = new InventorySaveData { items = items };
            var progress = SaveSystem.Load();
            progress.inventoryJson = JsonUtility.ToJson(saveData, true);
            SaveSystem.Save(progress);
        }

        public void AddItem(ItemDefinition itemDefinition)
        {
            if (itemDefinition == null)
            {
                return;
            }

            items.Add(new InventoryItem
            {
                itemId = System.Guid.NewGuid().ToString(),
                itemName = itemDefinition.itemName,
                rarity = itemDefinition.rarity,
                slot = itemDefinition.slot,
                attackBonus = itemDefinition.attackBonus,
                defenseBonus = itemDefinition.defenseBonus,
                healthBonus = itemDefinition.healthBonus,
                value = itemDefinition.value,
                description = itemDefinition.description
            });

            SaveInventory();
        }

        public InventoryItem AddRandomLoot(int battleTier)
        {
            var rarityRoll = UnityEngine.Random.Range(0, 100);
            ItemRarity rarity = ItemRarity.Common;
            if (battleTier >= 3 && rarityRoll > 80)
            {
                rarity = ItemRarity.Legendary;
            }
            else if (battleTier >= 2 && rarityRoll > 60)
            {
                rarity = ItemRarity.Epic;
            }
            else if (battleTier >= 1 && rarityRoll > 35)
            {
                rarity = ItemRarity.Rare;
            }

            var slot = (EquipmentSlot)UnityEngine.Random.Range(0, 4);
            var name = rarity + " " + slot + " of Valor";
            var item = new InventoryItem
            {
                itemId = System.Guid.NewGuid().ToString(),
                itemName = name,
                rarity = rarity,
                slot = slot,
                attackBonus = CalculateBonus(rarity, slot, battleTier, true),
                defenseBonus = CalculateBonus(rarity, slot, battleTier, false),
                healthBonus = Mathf.RoundToInt((battleTier + 1) * 5f + (int)rarity * 2),
                value = 25 + (int)rarity * 15 + battleTier * 10,
                description = "A reward from battle." 
            };

            items.Add(item);
            SaveInventory();
            return item;
        }

        public void EquipItem(string itemId)
        {
            var item = items.Find(x => x.itemId == itemId);
            if (item == null)
            {
                return;
            }

            if (equippedLookup.ContainsKey(item.slot))
            {
                equippedLookup[item.slot].equipped = false;
            }

            item.equipped = true;
            equippedLookup[item.slot] = item;
            SaveInventory();
        }

        public void UnequipItem(string itemId)
        {
            var item = items.Find(x => x.itemId == itemId);
            if (item == null)
            {
                return;
            }

            item.equipped = false;
            if (equippedLookup.ContainsKey(item.slot) && equippedLookup[item.slot].itemId == itemId)
            {
                equippedLookup.Remove(item.slot);
            }

            SaveInventory();
        }

        public void ApplyEquippedBonuses(ref int attack, ref int defense, ref int health)
        {
            foreach (var equipped in equippedLookup.Values)
            {
                attack += equipped.attackBonus;
                defense += equipped.defenseBonus;
                health += equipped.healthBonus;
            }
        }

        private void RebuildEquippedLookup()
        {
            equippedLookup.Clear();
            foreach (var item in items)
            {
                if (item.equipped)
                {
                    equippedLookup[item.slot] = item;
                }
            }
        }

        private int CalculateBonus(ItemRarity rarity, EquipmentSlot slot, int tier, bool attackBias)
        {
            int baseValue = (int)rarity + tier;
            if (attackBias)
            {
                return slot == EquipmentSlot.Weapon ? baseValue * 3 : baseValue + 1;
            }

            return slot == EquipmentSlot.Armor ? baseValue * 3 : baseValue + 1;
        }
    }
}
