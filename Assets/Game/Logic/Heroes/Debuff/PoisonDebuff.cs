using System.Collections;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    public class PoisonDebuff : MonoBehaviour, IDebuff
    {
        [SerializeField] private int _poisonDamage = 1;
        [SerializeField] private int _poisonTick = 5;
        [SerializeField, Min(1)]
        private float _durationDebuff;

        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed = false;

        public void Execute(Hero hero)
        {
            if (_isDebuffed == false)
            {
                _waitForSeconds = new WaitForSeconds(_durationDebuff);

                StartCoroutine(DebuffCoroutine(hero));
                Debug.Log($"Применен эффект Poison на {hero.name.Replace("(Clone)", string.Empty)}");
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
                Debug.Log($"Применен тик Poison на {hero.name.Replace("(Clone)", string.Empty)}");
                yield return _waitForSeconds;
            }

            _isDebuffed = false;
        }
    }
}