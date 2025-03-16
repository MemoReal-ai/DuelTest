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
        private bool _isDebuffed;
        private float _lastTimeAttacked;
        private Hero _target;
        private WaitForSeconds _waitForSecondsAttackCooldown;
        private bool _isAttacking;

        [field: SerializeField, Range(1, 100)]
        public int Damage { get; private set; } = 1;

        private void Start()
        {
            _waitForSecondsAttackCooldown = new WaitForSeconds(_attackCooldown);
            _debuffable = GetComponent<IDebuffable>();

            if (_debuffable == null) throw new Exception("Heroes has not been initialized");

            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth / _maxHealth);
            OnDamageChanged?.Invoke();

            _lastTimeAttacked = _attackCooldown;

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

        //Знаю что плохо.
        public void TakeDebuffPoison(int damage, float duration)
        {
            if (_isDebuffed == false)
                StartCoroutine(DebuffPoison(duration, damage));
        }

        public void TakeDebuffBush(float bushDuration)
        {
            if (_isDebuffed == false)
                StartCoroutine(DebuffBush(bushDuration, bushDuration));
        }

        public void TakeDebuffWeakly(float weakness, float duretion)
        {
            if (_isDebuffed == false)
                StartCoroutine(DebuffWeakly(duretion, weakness));
        }
        //Не знаю как хорошо ну и работает это так себе мне кажется((

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

        private IEnumerator DebuffWeakly(float duration, float statsAffect)
        {
            _isDebuffed = true;
            var defaultStats = Damage;
            var damageModifier = Damage * (1 - statsAffect);
            Damage = (int)damageModifier;
            OnDamageChanged?.Invoke();

            yield return new WaitForSeconds(duration);

            _isDebuffed = false;
            Damage = defaultStats;
            OnDamageChanged?.Invoke();
        }

        private IEnumerator DebuffPoison(float duration, int statsAffect)
        {
            _isDebuffed = true;
            var timer = 0f;
            while (timer < duration)
            {
                if (gameObject == null)
                    yield break;

                timer += Time.deltaTime;
                if (_target != null)
                {
                    _target.TakeDamage(statsAffect);
                    Debug.Log($"Deal poison {statsAffect}");
                    yield return null;
                }

                yield return null;
            }

            _isDebuffed = false;
        }


        private IEnumerator DebuffBush(float duration, float statsAffect)
        {
            _isDebuffed = true;
            var defaultStats = _lastTimeAttacked;
            _lastTimeAttacked += statsAffect;

            yield return new WaitForSeconds(duration);

            _isDebuffed = false;
            _lastTimeAttacked = defaultStats;
        }
        // ну и вот это соответственно тоже ((
    }
}