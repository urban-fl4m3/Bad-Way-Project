using System;
using UnityEngine;
using  UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Text _health;
    [SerializeField] private Image _fillBar;

    public void SetHealthInt(int health, int valueHealth)
    {
        _health.text = health+"/"+valueHealth;
        _fillBar.fillAmount = health * 1f / valueHealth;
    }
}
