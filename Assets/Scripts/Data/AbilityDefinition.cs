using UnityEngine;

namespace BattleLegends
{
    [CreateAssetMenu(menuName = "BattleLegends/Ability Definition")]
    public class AbilityDefinition : ScriptableObject
    {
        public string abilityName = "Basic Strike";
        public AbilityType abilityType = AbilityType.Damage;
        public float damageMultiplier = 1.2f;
        public float cooldown = 4f;
        public float range = 2.5f;
        public string description = "A basic battle ability.";
    }

    public enum AbilityType
    {
        Damage,
        Heal,
        Shield,
        Buff,
        Summon
    }
}
