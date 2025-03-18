using System.Collections;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    public class BushDebuff : MonoBehaviour, IDebuff
    {
        [SerializeField, Range(0, 1)]
        private float _bushChance = 0.5f;
        [SerializeField, Min(1)]
        private float _durationDebuff;


        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed = false;

        public void Execute(Hero hero)
        {
            var bush = Random.value;

            if (_bushChance > bush && _isDebuffed == false)
            {
                _waitForSeconds = new WaitForSeconds(_durationDebuff);
                StartCoroutine(DebuffCoroutine(hero));
            }
        }

        private IEnumerator DebuffCoroutine(Hero hero)
        {
            _isDebuffed = true;

            hero.IncreaseAttackCooldown(_durationDebuff);
            Debug.Log($"Применен эффект Bush на  {hero.name.Replace("(Clone)", string.Empty)}");

            yield return _waitForSeconds;
            hero.RestoreAttackCooldown();
            _isDebuffed = false;
        }
    }
}