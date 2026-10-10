using UnityEngine;

namespace BattleLegends
{
    public class CombatActor : MonoBehaviour
    {
        public string actorName = "Actor";
        public float maxHealth = 100f;
        public float health;
        public float damage = 10f;
        public float defense = 5f;
        public float attackSpeed = 1f;
        public float moveSpeed = 4f;
        public float attackRange = 1.6f;
        public bool isHero = false;
        public CombatActor target;

        protected float nextAttackTime;
        protected Renderer bodyRenderer;

        protected virtual void Awake()
        {
            health = maxHealth;
            bodyRenderer = GetComponent<Renderer>();
        }

        public virtual void TakeDamage(float amount)
        {
            float realDamage = Mathf.Max(0f, amount - defense * 0.5f);
            health -= realDamage;

            if (bodyRenderer != null)
            {
                bodyRenderer.material.color = Color.Lerp(Color.red, Color.white, health / Mathf.Max(1f, maxHealth));
            }

            if (health <= 0f)
            {
                health = 0f;
                Die();
            }
        }

        public virtual void BasicAttack(CombatActor enemy)
        {
            if (enemy == null || !enemy.gameObject.activeSelf)
            {
                return;
            }

            enemy.TakeDamage(damage + Random.Range(0f, 8f));
            nextAttackTime = Time.time + (1f / Mathf.Max(0.1f, attackSpeed));
        }

        public virtual void UseAbility(AbilityDefinition ability, CombatActor enemy)
        {
            if (ability == null || enemy == null)
            {
                return;
            }

            float damageAmount = damage * ability.damageMultiplier;
            enemy.TakeDamage(damageAmount);
        }

        protected virtual void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
