using UnityEngine;

namespace Game.Logic.Heroes
{
    public class Mage : Hero, IDebuffable
    {
        [SerializeField, Range(0, 1)]
        private float _weakCountPersent = 1;

        private float _weakDuration;

        public void DoDebuff(Hero hero, float duration)
        {
            _weakDuration = duration;
            hero.TakeDebuffWeakly(_weakCountPersent, _weakDuration);
            Debug.Log($"Примене эффект Weakly на {hero.GetType().Name}");
        }
    }
}