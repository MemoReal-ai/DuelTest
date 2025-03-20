using System;
using System.Collections;
using Game.Configs.Hero.Heroes;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Logic.Heroes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Hero : MonoBehaviour
    {
        public event Action<float> OnHealthChanged;
        public event Action<Hero> OnWin;
        public event Action OnDamageChanged;

        private NavMeshAgent _agent;
        private float _currentHealth;
        private IDebuff _debuff;
        private Hero _target;
        private WaitForSeconds _waitForSecondsAttackCooldown;
        private bool _isAttacking;
        private int _damageDefault;
        private float _attackCooldownDefault;

        [field: SerializeField]
        public HeroConfig HeroConfig { get; private set; }

        private void Start()
        {
            _waitForSecondsAttackCooldown = new WaitForSeconds(HeroConfig.AttackCooldown);
            _debuff = GetComponent<IDebuff>();

            if (_debuff == null)
            {
                throw new Exception("Heroes has not been initialized");
            }

            _currentHealth = HeroConfig.MaxHealth;
            OnHealthChanged?.Invoke(_currentHealth / HeroConfig.MaxHealth);

            _damageDefault = HeroConfig.Damage;
            _attackCooldownDefault = HeroConfig.AttackCooldown;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = HeroConfig.Speed;
        }

        private void Update()
        {
            if (_target == null)
            {
                OnWin?.Invoke(this);
                return;
            }

            if (!TargetInRange())
            {
                _agent.SetDestination(_target.transform.position);
            }
            else
            {
                _agent.SetDestination(transform.position);

                if (_isAttacking == false)
                {
                    StartCoroutine(Attack());
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
            }
        }

        public void DecreaseAttack(float percentDecrease)
        {
            HeroConfig.DecreaseAttack(percentDecrease);
            OnDamageChanged?.Invoke();
        }

        public void RestoreDamage()
        {
            HeroConfig.RestoreDamage(_damageDefault);
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

        private IEnumerator Attack()
        {
            _isAttacking = true;

            _target.TakeDamage(HeroConfig.Damage);
            _debuff.Execute(_target);

            yield return _waitForSecondsAttackCooldown;

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