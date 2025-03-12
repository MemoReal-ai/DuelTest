using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Logic.Heroes
{
    public class Mage : Heroes, IDebuffable
    {
        [SerializeField,Range(0, 1)] 
        private float _weakCountPersent = 1;
        
        private float _weakDuration;

        public void DoDebuff(Heroes hero, float duration)
        {
            _weakDuration = duration;
            hero.TakeDebuffWeakly(_weakCountPersent, _weakDuration);
            Debug.Log($"Примене эффект Weakly на {hero.GetType().Name}");
        }
    }
}