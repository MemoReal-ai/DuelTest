using System;
using Game.Logic.Heroes;
using UnityEngine;

namespace Game.Logic.Infrastructure
{
    [Serializable]
    public class HeroWinData
    {
        [field: SerializeField]
        public string Key { get; private set; }
        [field: SerializeField]
        public HeroConfig HeroConfig { get; private set; }
        
    }
}