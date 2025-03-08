using System;
using _project.Logic.Heroes;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
  [SerializeField] private Heroes hero;
  [SerializeField] private Image hpBar;

  private void Start()
  {
    hero.OnHealthChanged += UpdateHealth;
  }

  private void OnDestroy()
  {
    hero.OnHealthChanged -= UpdateHealth;
  }

  private void UpdateHealth(float health)
  {
    hpBar.fillAmount = health;
  }
}
