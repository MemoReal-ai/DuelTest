using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _project.Logic.Heroes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Heroes : MonoBehaviour
    {   [Header("Heroes stats")]
        [SerializeField,Range(1,100)] private int speed=1;
        [SerializeField, Range(1, 100)] private float attackCooldawn = 1f;
        [SerializeField, Range(1, 100)] private float rangeAttack = 1f;
        
        [SerializeField,Range(1,100)] protected int maxHealth;
        
        [field:SerializeField,Range(1,100)] public float Damage { get; private set; } =1;
        
    
        [Header("Debuffs Stats")]
        [SerializeField, Range(1, 15)] private float durationDebaff;
        
         
        
        private float _currentHealth; 
        private Heroes _target;
        private NavMeshAgent _agent;
        private float _lastTimeAttacked;
        private IDebufable _debuf;
        private bool _isDebuffed=false;
        
        
        public  event Action<float> OnHealthChanged;
        public event Action<Heroes> OnDeath;
        public event Action<Heroes> OnWin;
        public event Action OnDamageChanged;
        
        public void InitTarget(Heroes target)
        {
            _target=target;
        }
    
        
        protected virtual void Start()
        {
            _debuf=GetComponent<IDebufable>();
            
            if (_debuf == null)
            {
                throw new Exception("Heroes has not been initialized");
            }
            
            _currentHealth = maxHealth;
            InvokeOnHealthChanged();
          
            _lastTimeAttacked = attackCooldawn;
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;
           
            InvokeOnDamageChanged();
         
        }

        protected virtual void Update()
        {
            if (_target == null)
            {
                InvokeOnWin();
                return;
            }
            else
            {
                _agent.SetDestination(_target.transform.position); 
                
                Attack();
            }
        }




        public virtual void TakeDamage(int enemyDamage)
        {
            if(enemyDamage < 0)
                throw new Exception("You cannot take damage less than 0");
            
            _currentHealth = Mathf.Clamp(_currentHealth-Damage, 0, maxHealth);
            InvokeOnHealthChanged();
            
            if (_currentHealth==0)
            {  
                InvokeOnDeath();
                Destroy(gameObject);
            }
        }
        
        //Знаю что плохо.
        public void TakeDebuffPoisen(float damage,float duration)
        {
            if (_isDebuffed == false)
                StartCoroutine(DebuffPoison(duration,damage));
        }

        public void TakeDebuffBush(float bushDuraction)
        {
            if (_isDebuffed == false)
                StartCoroutine(DebuffBush(bushDuraction,bushDuraction));
        } 

        public void TakeDebuffWeakly(float weakness, float weaknessDuration)
        {  
            if (_isDebuffed == false)
                StartCoroutine(DebuffWeakly(weaknessDuration,weakness));
        }
        //Не знаю как хорошо ну и работает это так себе мне кажется((


        
        protected virtual void Attack()
        {   
            _lastTimeAttacked -= Time.deltaTime;
          
            if (Vector3.Distance(transform.position,_target.transform.position)<=rangeAttack && _lastTimeAttacked<=0)
            {
                _target.TakeDamage((int)Damage);
                _debuf.DoDebuff(_target,durationDebaff);
                _lastTimeAttacked = attackCooldawn;
            }
            
        }

        protected virtual void InvokeOnHealthChanged()
        {
            OnHealthChanged?.Invoke(_currentHealth/maxHealth);
        }

        protected virtual void InvokeOnDeath()
        {
            OnDeath?.Invoke(this);
        }

        protected virtual void InvokeOnWin()
        {
            OnWin?.Invoke(this);
        }

        protected virtual void InvokeOnDamageChanged()
        {
            OnDamageChanged?.Invoke();
        }

        private IEnumerator DebuffWeakly(float duration,float statsAffect)
        {   _isDebuffed=true;
            var defaultStats =Damage;
            Damage*=1-statsAffect;
            InvokeOnDamageChanged();
            yield return new WaitForSeconds(duration);
            _isDebuffed=false;
            Damage=defaultStats;
            InvokeOnDamageChanged();
        }
        private IEnumerator DebuffPoison(float duration,float statsAffect)
        {   _isDebuffed=true;
            var timer=0f;
            while (timer < duration)
            {  
                if(this.gameObject ==null)
                    yield break;
                
                timer+=Time.deltaTime;
                if (_target != null)
                {
                    _target.TakeDamage((int)statsAffect);
                    Debug.Log($"Deal poisen {statsAffect}");
                    yield return null;
                }
                
                yield return null;
            }
            
            _isDebuffed=false;
            
        }
        
        
        private IEnumerator DebuffBush(float duration,float statsAffect)
        {   _isDebuffed=true;
            var defaultStats = _lastTimeAttacked;
            _lastTimeAttacked+=statsAffect;
            yield return new WaitForSeconds(duration);
             _isDebuffed = false;
            _lastTimeAttacked=defaultStats;
        }
    // ну и вот это соответственно тоже ((
    }
}
