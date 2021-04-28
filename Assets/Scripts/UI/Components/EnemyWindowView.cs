using Modules.BattleModule;
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

        public void SetContext(string actorName, Sprite icon, string stats)
        {
            _name.text = actorName;
            _icon.sprite = icon;
            _stats.text = stats;
        }

        public void SetHealth(int health, int valueHealth, BattleActor enemyActor)
        {
            enemyActor.HealthChanged += OnHealthChange;
            _healthBar.SetHealthInt(health, valueHealth);
        }

        private void OnHealthChange(object sender, int e)
        {
            _healthBar.SetCurrentHealth(e);
        }
    }
}