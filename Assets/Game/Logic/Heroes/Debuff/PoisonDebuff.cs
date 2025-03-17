using System.Collections;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    [RequireComponent(typeof(Archer))]
    public class PoisonDebuff : MonoBehaviour, IDebuffable
    {
        [SerializeField] private int _poisonDamage = 1;
        [SerializeField] private int _poisonTick = 5;

        private float _poisonDuration;
        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed = false;

        public void DoDebuff(Hero hero, float duration)
        {
            if (_isDebuffed == false)
            {
                _poisonDuration = duration;
                _waitForSeconds = new WaitForSeconds(_poisonDuration);

                StartCoroutine(DebuffCoroutine(hero));
                Debug.Log($"Применен эффект Poison на {hero.GetType().Name}");
            }
        }

        private IEnumerator DebuffCoroutine(Hero hero)
        {
            _isDebuffed = true;
            for (int i = 0; i < _poisonTick; i++)
            {
                if (hero == null)
                {
                    break;
                }

                hero.TakeDamage(_poisonDamage);
                Debug.Log($"Применен тик Poison на {hero.GetType().Name}");
                yield return _waitForSeconds;
            }

            _isDebuffed = false;
        }
    }
}