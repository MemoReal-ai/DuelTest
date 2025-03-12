using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Heroes.Heroes _hero;
        [SerializeField] private Image _hpBar;

        private void Awake()
        {
            _hero.OnHealthChanged += UpdateHealth;
        }

        private void OnDestroy()
        {
            _hero.OnHealthChanged -= UpdateHealth;
        }

        private void UpdateHealth(float health)
        {
            _hpBar.fillAmount = health;
        }
    }
}