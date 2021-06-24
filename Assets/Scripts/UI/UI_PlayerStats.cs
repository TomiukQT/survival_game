using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;



public class UI_PlayerStats : MonoBehaviour
{
   
   //UI Elements
   //TODO : Make wrapper for sliders
   [SerializeField] private Image _healthBar;
   [SerializeField] private TextMeshProUGUI _healthBarText;

   [SerializeField] private Image _manaBar;
   [SerializeField] private TextMeshProUGUI _manaBarText;
   
   
   //References
   [SerializeField] private Player _player;
   
   
   //Fields
   private Resource _health;
   private Resource _mana;
   
   private void Awake()
   {
      _health = _player.Health;
      _health.OnResourceChange += e_OnHealthChange;

      _mana = _player.Mana;
      _mana.OnResourceChange += e_OnManaChange;
   }
   
   private void Start()
   {
      e_OnHealthChange(null,null);
      e_OnManaChange(null,null);
   }

   private void e_OnHealthChange(object sender, EventArgs e)
   {
      _healthBar.fillAmount = _health.Percentage01;
      _healthBarText.text = _health.Value + " / "  + _health.MaxValue;
   }
   
   private void e_OnManaChange(object sender, EventArgs e)
   {
      _manaBar.fillAmount = _mana.Percentage01;
      _manaBarText.text = _mana.Value + " / " + _mana.MaxValue;
   }
   
}
