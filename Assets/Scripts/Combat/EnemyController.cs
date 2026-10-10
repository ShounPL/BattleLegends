using UnityEngine;

namespace BattleLegends
{
    public class EnemyController : CombatActor
    {
        public string enemyName = "Wild Beast";

        protected override void Awake()
        {
            base.Awake();
            isHero = false;
        }

        public void Configure(string name, float hp, float damageValue, float speed)
        {
            actorName = name;
            enemyName = name;
            maxHealth = hp;
            health = hp;
            damage = damageValue;
            defense = 3f;
            moveSpeed = speed;
            attackSpeed = 1.1f;
            attackRange = 1.8f;

            if (bodyRenderer != null)
            {
                bodyRenderer.material.color = Color.red;
            }
        }

        private void Update()
        {
            if (!BattleManager.Instance || !BattleManager.Instance.battleActive)
            {
                return;
            }

            if (target == null || !target.gameObject.activeSelf)
            {
                target = FindNearestHero();
            }

            if (target == null)
            {
                return;
            }

            Vector3 targetDirection = target.transform.position - transform.position;
            float distance = targetDirection.magnitude;

            if (distance > attackRange)
            {
                transform.position += targetDirection.normalized * moveSpeed * Time.deltaTime;
            }
            else if (Time.time >= nextAttackTime)
            {
                BasicAttack(target);
            }
        }

        private CombatActor FindNearestHero()
        {
            CombatActor closest = null;
            float closestDistance = float.MaxValue;

            foreach (var hero in BattleManager.Instance.heroes)
            {
                if (hero == null || !hero.gameObject.activeSelf)
                {
                    continue;
                }

                float distance = Vector3.Distance(transform.position, hero.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = hero;
                }
            }

            return closest;
        }
    }
}
