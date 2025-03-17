using System.Collections;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    [RequireComponent(typeof(Mage))]
    public class WeakDebuff : MonoBehaviour, IDebuffable
    {
        [SerializeField, Range(0, 1)]
        private float _weakCountPerсent = 1;

        private float _weakDuration;
        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed = false;

        public void DoDebuff(Hero hero, float duration)
        {
            _weakDuration = duration;
            _waitForSeconds = new WaitForSeconds(_weakDuration);

            if (_isDebuffed == false)
            {
                StartCoroutine(DebuffCoroutine(hero));
            }
        }

        private IEnumerator DebuffCoroutine(Hero hero)
        {
            _isDebuffed = true;

            hero.DecreaseAttack(_weakCountPerсent);
            Debug.Log($"Примене эффект Weakly на {hero.GetType().Name}");

            yield return _waitForSeconds;
            hero.RestoreDamage();
            _isDebuffed = false;
        }
    }
}