using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Logic.Heroes
{
    public class Barbarian : Heroes, IDebuffable
    {
        [Header("BarbarianStats"),SerializeField,Range(0, 1)]
        private float _bushChance = 0.5f;

        private float _bushDuration;
        
        public void DoDebuff(Heroes hero, float duration)
        {
             _bushDuration=duration;
            var bush = Random.value;
            if (_bushChance > bush)
            {
                hero.TakeDebuffBush(_bushDuration);
                Debug.Log($"Применен эффект Bush на  {hero.GetType().Name}");
            }
        }
    }
}