using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class EnemyWindow: MonoBehaviour

    {
        [SerializeField] private Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private Text _stats;
        [SerializeField] private HealthBar _healthBar;

        public void SetContext(string name, Sprite icon, string stats, int health, int valueHealth)
        {
            _name.text = name;
            _icon.sprite = icon;
            _stats.text = stats;
            _healthBar.SetHealthInt(health, valueHealth);
        }
        
    }
}