using Modules.ActorModule;
using Modules.BattleModule;
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

        public void SetContext(string name, Sprite icon, string stats)
        {
            _name.text = name;
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