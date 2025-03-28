using Game.Logic.SpawnerHeroes;
using Game.Logic.UI.HeroView;
using Game.Logic.UI.WinPanelView;
using UnityEngine;
using Zenject;

namespace Game.Logic.Infrastructure.Installer
{
    public class EntryPointInstaller : MonoInstaller
    {
        [SerializeField] private SpawnerConfig _spawnerConfig;
        [SerializeField] private StatsHeroView _statsHeroView;
        [SerializeField] private WinPanelView _winPanelView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EntryPoint>().AsSingle();
            Container.Bind<DataHandler>().FromComponentInHierarchy().AsSingle();
           
            var sceneHandler=new SceneHandler();
            Container.Bind<SceneHandler>().FromInstance(sceneHandler).AsSingle();
            
            InstallSpawner();
            InstallUI();
        }

        private void InstallSpawner()
        {
            Container.Bind<SpawnerConfig>().FromInstance(_spawnerConfig).AsSingle();
            Container.Bind<SpawnPoints>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallUI()
        {
            Container.Bind<StatsHeroView>().FromInstance(_statsHeroView).AsSingle();
            _winPanelView = Container.InstantiatePrefabForComponent<WinPanelView>(_winPanelView);
            Container.Bind<WinPanelView>().FromInstance(_winPanelView).AsSingle();
        }
    }
}