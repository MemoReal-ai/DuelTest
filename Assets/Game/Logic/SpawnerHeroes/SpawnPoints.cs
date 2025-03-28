using UnityEngine;

namespace Game.Logic.SpawnerHeroes
{
    public class SpawnPoints : MonoBehaviour
    {
        [field: SerializeField]
        public Transform[] Points { get; private set; }
    }
}