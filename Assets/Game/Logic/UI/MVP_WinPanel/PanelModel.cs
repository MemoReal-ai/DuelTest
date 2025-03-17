using Game.Logic.Heroes;
using Game.Logic.SpawnerHeroes;
using UnityEngine;

namespace Game.Logic.UI.MVP_WinPanel
{
    public class PanelModel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private WinPanelView _winPanelView;

        private PanelPresenter _presenter;

        private void Start()
        {
            _presenter = new PanelPresenter(_panel, _winPanelView);
            foreach (var hero in _spawner.ContainerHeroes)
            {
                hero.OnWin += Show;
            }
        }

        private void OnDestroy()
        {
            foreach (var hero in _spawner.ContainerHeroes)
            {
                hero.OnWin -= Show;
            }
        }

        private void Show(Hero hero)
        {
            _presenter.Show(hero);
        }
    }
}