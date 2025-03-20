using System.Collections.Generic;
using UnityEngine;
using Game.Logic.Heroes;

namespace Game.Logic.SpawnerHeroes
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Hero[] _heroes;
        [SerializeField] private Transform[] _spawnPoints;

        private readonly List<Hero> _containerHeroes = new();

        public IEnumerable<Hero> ContainerHeroes => _containerHeroes;

        private void Awake()
        {
            CreateRandomHero();
            SetTargetHero();
        }

        private void CreateRandomHero()
        {
            for (var i = 0; i < _spawnPoints.Length; i++)
            {
                var randomIndex = Random.Range(0, _heroes.Length);
                var randomHero = Instantiate(_heroes[randomIndex], _spawnPoints[i].position, Quaternion.identity);
                _containerHeroes.Add(randomHero);
            }
        }

        private void SetTargetHero()
        {
            _containerHeroes[0].InitTarget(_containerHeroes[1]);
            _containerHeroes[1].InitTarget(_containerHeroes[0]);
        }
    }
}