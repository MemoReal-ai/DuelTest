using Game.Logic.Heroes;
using TMPro;
using UnityEngine;

namespace Game.Logic.UI.MVP_WinPanel
{
    public class WinPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void ShowWinName(Hero hero)
        {
            _text.text = hero.GetType().Name + "\n winning";
        }
    }
}