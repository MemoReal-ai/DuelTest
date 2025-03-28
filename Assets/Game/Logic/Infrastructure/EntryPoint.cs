using System;
using Game.Logic.SpawnerHeroes;
using Game.Logic.UI.HeroView;
using Game.Logic.UI.WinPanelView;
using Zenject;

namespace Game.Logic.Infrastructure
{
    public class EntryPoint : IInitializable, IDisposable
    {
        private readonly SpawnerConfig _spawnerConfig;
        private readonly SpawnPoints _spawnPoints;
        private readonly StatsHeroView _statsHeroView;
        private readonly WinPanelView _winPanelView;
        private readonly DataHandler _dataHandler;
        private PanelUIFactory _panelUIFactory;
        private HeroUIFactory _heroUIFactory;
        private Spawner _spawner;

        public EntryPoint(WinPanelView winPanelView,
            SpawnerConfig spawnerConfig,
            SpawnPoints spawnPoints,
            StatsHeroView statsHeroView, DataHandler dataHandler)
        {
            _winPanelView = winPanelView;
            _spawnerConfig = spawnerConfig;
            _spawnPoints = spawnPoints;
            _statsHeroView = statsHeroView;
            _dataHandler = dataHandler;
        }

        public void Initialize()
        {
            _dataHandler.LoadData();

            _spawner = new Spawner(_spawnerConfig, _spawnPoints, _statsHeroView);
            _spawner.SpawnHeroes();

            var sceneHandler = new SceneHandler();

            _panelUIFactory = new PanelUIFactory(_spawner, _winPanelView, sceneHandler);
            _panelUIFactory.Enable();
        }

        public void Dispose()
        {
            _panelUIFactory.Disable();
            _spawner.Disable();
            _dataHandler.SaveData();
        }
    }
}