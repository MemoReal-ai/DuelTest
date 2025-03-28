using System.Collections.Generic;
using UnityEngine;
using Game.Logic.Heroes;
using Game.Logic.UI.HeroView;

namespace Game.Logic.SpawnerHeroes
{
    public class Spawner
    {
        private readonly SpawnerConfig _config;
        private readonly SpawnPoints _spawnPoints;
        private readonly StatsHeroView _statsHeroView;
        private readonly List<Hero> _containerHeroes = new();

        private HeroUIFactory _heroUIFactory;
        public IEnumerable<Hero> ContainerHeroes => _containerHeroes;

        public Spawner(SpawnerConfig config, SpawnPoints spawnPoints, StatsHeroView statsHeroView)
        {
            _config = config;
            _spawnPoints = spawnPoints;
            _statsHeroView = statsHeroView;
        }

        public void SpawnHeroes()
        {
            CreateRandomHero();
            SetTargetHero();
        }

        private void CreateRandomHero()
        {
            foreach (var t in _spawnPoints.Points)
            {
                var randomIndex = Random.Range(0, _config.Heroes.Length);
                var randomHero =
                    Object.Instantiate(_config.Heroes[randomIndex],
                        t.position,
                        Quaternion.identity);

                _heroUIFactory = new HeroUIFactory(randomHero, _statsHeroView);
                _heroUIFactory.Enable();
                
                _containerHeroes.Add(randomHero);
            }
        }

        private void SetTargetHero()
        {
            _containerHeroes[0].InitTarget(_containerHeroes[1]);
            _containerHeroes[1].InitTarget(_containerHeroes[0]);
        }

        public void Disable()
        {
            _heroUIFactory.Disable();
        }
    }
}