using Game.Logic.Heroes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic.UI.MVP
{
    public class StatsHeroView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _hpBar;

        public void UpdateHealth(float health)
        {
            _hpBar.fillAmount = health;
        }
    
        public void ShowDamage(Hero hero)
        {
            _text.text = hero.Damage.ToString();
        }
    }
}