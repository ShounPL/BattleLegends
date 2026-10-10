using UnityEngine;

namespace BattleLegends
{
    [CreateAssetMenu(menuName = "BattleLegends/Hero Definition")]
    public class HeroDefinition : ScriptableObject
    {
        public string heroName = "Astra";
        public HeroClass heroClass = HeroClass.Warrior;
        public HeroRarity rarity = HeroRarity.Rare;
        public int maxHealth = 120;
        public int attackDamage = 20;
        public int defense = 8;
        public float attackSpeed = 1.2f;
        public float moveSpeed = 4f;
        public string primaryRole = "Frontline";
        public AbilityDefinition[] abilities;
    }

    public enum HeroClass
    {
        Warrior,
        Assassin,
        Mage,
        Archer,
        Tank,
        Healer,
        Summoner,
        Ranger,
        Necromancer,
        Support
    }

    public enum HeroRarity
    {
        Common,
        Rare,
        Epic,
        Legendary,
        Mythic
    }
}
