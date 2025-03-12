using TMPro;
using UnityEngine;

namespace Game.Logic.UI
{
    public class DamagaView : MonoBehaviour
    {
        [SerializeField] private Heroes.Heroes _hero;
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            _hero.OnDamageChanged += ShowDamage;
        }

        private void OnDestroy()
        {
            _hero.OnDamageChanged -= ShowDamage;
        }

        private void ShowDamage()
        {
            _text.text = _hero.Damage.ToString();
        }
    }
}