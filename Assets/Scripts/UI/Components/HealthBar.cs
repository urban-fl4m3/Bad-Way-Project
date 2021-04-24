using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Text _health;
        [SerializeField] private Image _fillBar;
    
        private int _valueHealth;
    
        public void SetHealthInt(int health, int valueHealth)
        {
            _valueHealth = valueHealth;
            _health.text = health+"/"+_valueHealth;
            _fillBar.fillAmount = health * 1f / _valueHealth;
        }

        public void SetCurrentHealth(int health)
        {
            _health.text = health+"/"+_valueHealth;
            _fillBar.fillAmount = health * 1f / _valueHealth;
        }
    }
}
