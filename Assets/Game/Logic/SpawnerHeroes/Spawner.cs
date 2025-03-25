using System.Collections.Generic;
using UnityEngine;
using Game.Logic.Heroes;
using Game.Logic.Infrastructure;
using Game.Logic.UI.HeroView;

namespace Game.Logic.SpawnerHeroes
{
    public class Spawner
    {
        private readonly CoroutineLauncher _launcher;
        private readonly SpawnerConfig _config;
        private readonly Transform[] _spawnPoints;
        private readonly StatsHeroView _statsHeroView;
        private readonly List<Hero> _containerHeroes = new();

        private HeroUIFactory _heroUIFactory;
        public IEnumerable<Hero> ContainerHeroes => _containerHeroes;

        public Spawner(SpawnerConfig config, Transform[] spawnPoints, StatsHeroView statsHeroView,
            CoroutineLauncher launcher)
        {
            _config = config;
            _spawnPoints = spawnPoints;
            _statsHeroView = statsHeroView;
            _launcher = launcher;
        }

        public void SpawnHeroes()
        {
            CreateRandomHero();
            SetTargetHero();
        }

        private void CreateRandomHero()
        {
            for (var i = 0; i < _spawnPoints.Length; i++)
            {
                var randomIndex = Random.Range(0, _config.Heroes.Length);
                var randomHero =
                    Object.Instantiate(_config.Heroes[randomIndex], _spawnPoints[i].position, Quaternion.identity);
                
                randomHero.InitCoroutineLauncher(_launcher);
                
                _heroUIFactory = new HeroUIFactory(randomHero, _statsHeroView);
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