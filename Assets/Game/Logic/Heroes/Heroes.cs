using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


namespace Game.Logic.Heroes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Heroes : MonoBehaviour
    {
        public event Action<float> OnHealthChanged;
        public event Action<Heroes> OnWin;
        public event Action OnDamageChanged;
        
        [Header("Heroes stats"),SerializeField,Range(1, 100)]
        private int speed = 1; 
        [SerializeField,Range(1, 100)]
        private float _attackCooldown = 1f;
        [SerializeField,Range(1, 100)]
        private float rangeAttack = 1f;
        [SerializeField,Range(1, 15)]
        private float durationDebaff;
        
        [SerializeField,Range(1, 100)]
        protected int maxHealth;
       
        private NavMeshAgent _agent;
        private float _currentHealth;
        private IDebuffable _debuffable;
        private bool _isDebuffed;
        private float _lastTimeAttacked;
        private Heroes _target;
        private WaitForSeconds _waitForSeconds;


        [field: SerializeField,Range(1, 100)]
        public float Damage { get; private set; } = 1;
        
        protected  void Start()
        {
            _debuffable = GetComponent<IDebuffable>();

            if (_debuffable == null) throw new Exception("Heroes has not been initialized");

            _currentHealth = maxHealth;
            InvokeOnHealthChanged();

            _lastTimeAttacked = _attackCooldown;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;

            InvokeOnDamageChanged();
        }

        protected  void Update()
        {
            if (_target == null)
            {
                InvokeOnWin();
                return;
            }

            _agent.SetDestination(_target.transform.position);
            Attack();
        }

  

        public void InitTarget(Heroes target)
        {
            _target = target;
        }


        public void TakeDamage(int enemyDamage)
        {
            if (enemyDamage < 0)
                throw new Exception("You cannot take damage less than 0");

            _currentHealth = Mathf.Clamp(_currentHealth - enemyDamage, 0, maxHealth);
            InvokeOnHealthChanged();

            if (_currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }

        //Знаю что плохо.
        public void TakeDebuffPoisen(int damage, float duration)
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

        private void Attack()
        {
            _lastTimeAttacked -= Time.deltaTime;

            if (Vector3.Distance(transform.position, _target.transform.position) <= rangeAttack &&
                _lastTimeAttacked <= 0)
            {
                _target.TakeDamage((int)Damage);
                _debuffable.DoDebuff(_target, durationDebaff);
                _lastTimeAttacked = _attackCooldown;
            }
        }

        private void InvokeOnHealthChanged()
        {
            OnHealthChanged?.Invoke(_currentHealth / maxHealth);
        }

        private void InvokeOnWin()
        {
            OnWin?.Invoke(this);
        }

        private void InvokeOnDamageChanged()
        {
            OnDamageChanged?.Invoke();
        }

        private IEnumerator DebuffWeakly(float duration, float statsAffect)
        {
            _isDebuffed = true;
            var defaultStats = Damage;
            Damage *= 1 - statsAffect;
            InvokeOnDamageChanged();

            yield return new WaitForSeconds(duration);

            _isDebuffed = false;
            Damage = defaultStats;
            InvokeOnDamageChanged();
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