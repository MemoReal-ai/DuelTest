using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Logic.Heroes
{
    public class Barbarian :Heroes,IDebufable
    {   [Header("BarbarianStats")]
        [SerializeField,Range(0,1)] private float bushChance = 0.5f;
        [SerializeField] private float bushDuration=4f;

        

        public void DoDebuff(Heroes hero,float duration)
        {
            var bush = Random.value;
            if (bushChance > bush)
            {
                hero.TakeDebuffBush(bushDuration);
                Debug.Log($"Применен эффект Bush на  {hero.GetType().Name}");
              
            }
        }
    }
}