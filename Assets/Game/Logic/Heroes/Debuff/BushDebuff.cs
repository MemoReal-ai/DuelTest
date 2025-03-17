using System.Collections;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    [RequireComponent(typeof(Barbarian))]
    public class BushDebuff : MonoBehaviour, IDebuffable
    {
        [SerializeField, Range(0, 1)]
        private float _bushChance = 0.5f;

        private float _bushDuration;
        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed = false;

        public void DoDebuff(Hero hero, float duration)
        {
            _bushDuration = duration;
            var bush = Random.value;

            if (_bushChance > bush && _isDebuffed == false)
            {
                _waitForSeconds = new WaitForSeconds(_bushDuration);
                StartCoroutine(DebuffCoroutine(hero));
            }
        }

        private IEnumerator DebuffCoroutine(Hero hero)
        {
            _isDebuffed = true;

            hero.IncreaseAttackCooldawn(_bushDuration);
            Debug.Log($"Применен эффект Bush на  {hero.GetType().Name}");

            yield return _waitForSeconds;
            hero.RestoreAttackCooldawn();
            _isDebuffed = false;
        }
    }
}