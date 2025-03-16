using System;
using System.Collections;
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

        [Header("Heroes stats"), SerializeField, Range(1, 100)]
        private int _speed = 1;
        [SerializeField, Range(1, 100)]
        private float _attackCooldown = 1f;
        [SerializeField, Range(1, 100)]
        private float _rangeAttack = 1f;
        [SerializeField, Range(1, 15)]
        private float _durationDebaff;

        [SerializeField, Range(1, 100)]
        protected int _maxHealth;

        private NavMeshAgent _agent;
        private float _currentHealth;
        private IDebuffable _debuffable;
        private Hero _target;
        private WaitForSeconds _waitForSecondsAttackCooldown;
        private bool _isAttacking;
        private int _damageDefault;
        private float _attackCooldownDefault;

        [field: SerializeField, Range(1, 100)]
        public int Damage { get; private set; } = 1;

        private void Start()
        {
            _waitForSecondsAttackCooldown = new WaitForSeconds(_attackCooldown);
            _debuffable = GetComponent<IDebuffable>();

            if (_debuffable == null)
            {
                throw new Exception("Heroes has not been initialized");
            }

            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth / _maxHealth);
            OnDamageChanged?.Invoke();

            _damageDefault = Damage;
            _attackCooldownDefault = _attackCooldown;
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _speed;
        }

        private void Update()
        {
            if (_target == null)
            {
                OnWin?.Invoke(this);
                return;
            }

            _agent.SetDestination(_target.transform.position);

            if (TargetInRange() && _isAttacking == false)
            {
                StartCoroutine(Attack());
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

            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth / _maxHealth);

            if (_currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }

        public void DecreaseAttack(float percentDecrease)
        {
            Damage = (int)(Damage * (1 - percentDecrease));
            OnDamageChanged?.Invoke();
        }

        public void RestoreDamage()
        {
            Damage = _damageDefault;
            OnDamageChanged?.Invoke();
        }

        public void IncreaseAttackCooldawn(float timeToIncrease)
        {
            _attackCooldown += timeToIncrease;
        }

        public void RestoreAttackCooldawn()
        {
            _attackCooldown = _attackCooldownDefault;
        }
        
        private IEnumerator Attack()
        {
            _isAttacking = true;

            _target.TakeDamage(Damage);
            _debuffable.DoDebuff(_target, _durationDebaff);

            yield return _waitForSecondsAttackCooldown;

            _isAttacking = false;
        }

        private bool TargetInRange()
        {
            var distanceToTarget = (_target.transform.position - transform.position).sqrMagnitude;
            if (distanceToTarget <= _rangeAttack * _rangeAttack)
            {
                return true;
            }

            return false;
        }
    }
}