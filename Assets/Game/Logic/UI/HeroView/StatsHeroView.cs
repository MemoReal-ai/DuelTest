using Game.Logic.Heroes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic.UI.HeroView
{
    public class StatsHeroView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _hpBar;

        public void UpdateHealth(float health)
        {
            _hpBar.fillAmount = health;
        }
    
        public void ShowDamage(int heroDamage)
        {
            _text.text = heroDamage.ToString();
        }
    }
}