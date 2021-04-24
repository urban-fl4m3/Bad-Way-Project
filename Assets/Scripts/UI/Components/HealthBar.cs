using System;
using UnityEngine;
using  UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Text _health;
    [SerializeField] private Image _fillBar;
    private int _valueHealh;
    public void SetHealthInt(int health, int valueHealth)
    {
        _valueHealh = valueHealth;
        _health.text = health+"/"+_valueHealh;
        _fillBar.fillAmount = health * 1f / _valueHealh;
    }

    public void SetCurrentHealth(int health)
    {
        _health.text = health+"/"+_valueHealh;
        _fillBar.fillAmount = health * 1f / _valueHealh;
    }
}
