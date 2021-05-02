using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class EnemyWindowView: MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private Text _stats;
        [SerializeField] private HealthBar _healthBar;

        private int _maxHealth;
        private DynamicValue<int> _health;
        
        public void SetContext(string actorName, Sprite icon, string stats)
        {
            _name.text = actorName;
            _icon.sprite = icon;
            _stats.text = stats;
        }

        public void SetHealth(DynamicValue<int> health, int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = health;
            _healthBar.SetHealthInt(health.Value, maxHealth);

            health.Changed += HandleHealthChanged;
        }

        private void HandleHealthChanged(object sender, int e)
        {
            _healthBar.SetHealthInt(_health.Value,_maxHealth);
        }
    }
}