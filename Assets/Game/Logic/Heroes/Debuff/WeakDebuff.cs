using System.Collections;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    public class WeakDebuff : MonoBehaviour, IDebuff
    {
        [SerializeField, Range(0, 1)]
        private float _weakCountPerсent = 1;
        [SerializeField, Min(0)]
        private float _durationDebuff;

        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed = false;

        public void Execute(Hero hero)
        {
            _waitForSeconds = new WaitForSeconds(_durationDebuff);

            if (_isDebuffed == false)
            {
                StartCoroutine(DebuffCoroutine(hero));
            }
        }

        private IEnumerator DebuffCoroutine(Hero hero)
        {
            _isDebuffed = true;

            hero.DecreaseAttack(_weakCountPerсent);
            Debug.Log($"Примене эффект Weakly на {hero.name.Replace("(Clone)", string.Empty)}");

            yield return _waitForSeconds;
            hero.RestoreDamage();
            _isDebuffed = false;
        }
    }
}