using Game.Logic.Heroes;
using UnityEngine;

namespace Game.Logic.SpawnerHeroes
{
    [CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Config/SpawnerConfig")]
    public class SpawnerConfig : ScriptableObject
    {
        [field: SerializeField]  public Hero[] Heroes { get; private set; }
    }
}