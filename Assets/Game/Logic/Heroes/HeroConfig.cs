using UnityEngine;

namespace Game.Logic.Heroes
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Config/Hero Config")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)]
        public int Speed { get; private set; } = 1;

        [field: SerializeField, Min(1)]
        public float AttackCooldown { get; private set; } = 1f;

        [field: SerializeField, Min(1)]
        public float RangeAttack { get; private set; } = 1f;

        [field: SerializeField, Min(1)]
        public int MaxHealth { get; private set; } = 1;

        [field: SerializeField, Min(0)]
        public int Damage { get; private set; } = 1;

        [field: SerializeField,Min(0)]
        public int WinCount { get; private set; }

        public void IncreaseAttackCooldown(float timeToIncrease)
        {
            AttackCooldown += timeToIncrease;
        }

        public int RestoreDamage()
        {
            return Damage;
        }

        public void IncreaseWinCount()
        {
            WinCount++;
        }

        public void RestoreAttackCooldown(float attackCooldownDefault)
        {
            AttackCooldown = attackCooldownDefault;
        }

        public void SetWinCount(int value)
        {
            WinCount = value;
        }
    }
}