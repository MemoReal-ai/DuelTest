using System;
using _project.Logic.Heroes;
using TMPro;
using UnityEngine;

public class DamagaView : MonoBehaviour
{
    [SerializeField] private Heroes hero;
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        hero.OnDamageChanged+=ShowDamage;
    }

    private void OnDestroy()
    {
        hero.OnDamageChanged-=ShowDamage;
    }

    private void ShowDamage()
    {
        text.text = hero.Damage.ToString();
        
    }
}
