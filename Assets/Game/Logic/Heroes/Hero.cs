using System;
using Cysharp.Threading.Tasks;
using Game.Logic.Heroes.Debuff;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Logic.Heroes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Hero : MonoBehaviour
    {
        public event Action<float> OnHealthChanged;
        public event Action<Hero> OnWin;
        public event Action OnDamageChanged;

        private NavMeshAgent _agent;
        private float _currentHealth;
        private Hero _target;
        private WaitForSeconds _waitForSecondsAttackCooldown;
        private bool _isAttacking;
        private float _attackCooldownDefault;

        protected IDebuff Debuff;

        [field: SerializeField]
        public HeroConfig HeroConfig { get; private set; }
        public int CurrentDamage { get; private set; }

        private void Awake()
        {
            CurrentDamage = HeroConfig.Damage;
        }

        protected virtual void Start()
        {
            _currentHealth = HeroConfig.MaxHealth;
            OnHealthChanged?.Invoke(_currentHealth / HeroConfig.MaxHealth);

            _attackCooldownDefault = HeroConfig.AttackCooldown;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = HeroConfig.Speed;
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            if (!TargetInRange())
            {
                _agent.SetDestination(_target.transform.position);
            }
            else
            {
                _agent.isStopped = TargetInRange();

                if (_isAttacking == false)
                {
                    _ = Attack();
                }
            }
        }

        public void InitTarget(Hero target)
        {
            _target = target;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new Exception("You cannot take damage less than 0");

            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, HeroConfig.MaxHealth);
            OnHealthChanged?.Invoke(_currentHealth / HeroConfig.MaxHealth);

            if (_currentHealth == 0)
            {
                Destroy(gameObject);
                _target.HeroConfig.IncreaseWinCount();
                OnWin?.Invoke(_target);
            }
        }

        public void DecreaseAttack(float percentDecrease)
        {
            CurrentDamage = (int)(CurrentDamage * (1 - percentDecrease));
            OnDamageChanged?.Invoke();
        }

        public void RestoreDamage()
        {
            CurrentDamage = HeroConfig.RestoreDamage();
            OnDamageChanged?.Invoke();
        }

        public void IncreaseAttackCooldown(float timeToIncrease)
        {
            HeroConfig.IncreaseAttackCooldown(timeToIncrease);
        }

        public void RestoreAttackCooldown()
        {
            HeroConfig.RestoreAttackCooldown(_attackCooldownDefault);
        }

        private async UniTask Attack()
        {
            _isAttacking = true;

            _target.TakeDamage(CurrentDamage);
            Debuff.Execute(_target);

            await UniTask.Delay(TimeSpan.FromSeconds(_attackCooldownDefault));
            _isAttacking = false;
        }

        private bool TargetInRange()
        {
            var distanceToTarget = (_target.transform.position - transform.position).sqrMagnitude;
            if (distanceToTarget <= HeroConfig.RangeAttack * HeroConfig.RangeAttack)
            {
                return true;
            }

            return false;
        }
    }
}