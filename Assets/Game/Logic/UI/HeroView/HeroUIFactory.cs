using Game.Logic.Heroes;
using UnityEngine;


namespace Game.Logic.UI.HeroView
{
    public class HeroUIFactory : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        [SerializeField] private StatsHeroView _statsHeroView;

        private Presenter _presenter;

        private void Awake()
        {
            _presenter = new Presenter(_statsHeroView, _hero);
            _presenter.Enable();
            _presenter.SetDamageView();
        }

        private void OnDestroy()
        {
            _presenter.Disable();
        }
    }
}