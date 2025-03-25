using System;
using Game.Logic.SpawnerHeroes;
using Game.Logic.UI.HeroView;
using Game.Logic.UI.WinPanelView;
using UnityEngine;
using Zenject;

namespace Game.Logic.Infrastructure
{
    public class Bootstrap : IInitializable, IDisposable
    {
        private readonly WinPanelView _winPanelView;
        private PanelUIFactory _panelUIFactory;
        private HeroUIFactory _heroUIFactory;
        private Spawner _spawner;


        public Bootstrap(WinPanelView winPanelView,Spawner spawner)
        {
            _winPanelView = winPanelView;
            _spawner = spawner;
        }

        public void Initialize()
        {
            var sceneHandler = new SceneHandler();
            _panelUIFactory = new PanelUIFactory(_spawner, _winPanelView, sceneHandler);
        }

        public void Dispose()
        {
            _panelUIFactory.Disable();
            _spawner.Disable();
        }
    }
}