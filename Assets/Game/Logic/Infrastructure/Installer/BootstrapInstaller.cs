using Game.Logic.SpawnerHeroes;
using Game.Logic.UI.HeroView;
using Game.Logic.UI.WinPanelView;
using UnityEngine;
using Zenject;

namespace Game.Logic.Infrastructure.Installer
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SpawnerConfig _spawnerConfig;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private StatsHeroView _statsHeroView;
        [SerializeField] private WinPanelView _winPanelView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Bootstrap>().AsCached();
            InstallSpawner();
            InstallUI();
        }

        private void InstallSpawner()
        {  
            Container.Bind<CoroutineLauncher>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SpawnerConfig>().FromInstance(_spawnerConfig).AsSingle();
            Container.Bind<Transform[]>().FromInstance(_spawnPoints).AsSingle();
            
            var launcher = Container.Resolve<CoroutineLauncher>();
            var spawner = new Spawner(_spawnerConfig, _spawnPoints,_statsHeroView,launcher);
            Container.BindInstance(spawner).AsSingle();
            spawner.SpawnHeroes();
        }

        private void InstallUI()
        {
            Container.Bind<StatsHeroView>().FromInstance(_statsHeroView).AsSingle();
            Container.Bind<WinPanelView>().FromInstance(_winPanelView).AsSingle();
        }
    }
}