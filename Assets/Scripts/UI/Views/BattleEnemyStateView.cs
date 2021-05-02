using Modules.BattleModule;
using Modules.BattleModule.Stats.Helpers;
using UI.Components;
using UI.Interface;
using UI.Models;
using UnityEngine;

namespace UI.Views
{
    public class BattleEnemyStateView : MonoBehaviour, ICanvasView, IViewModel
    {
        [SerializeField] private EnemyWindowView enemyWindowView;
        
        public Canvas Canvas { get; set; }
        public GameObject GameObject => gameObject;
        private BattleEnemyStateModel _model;

        public void ResolveModel(IModel model)
        {
            _model = (BattleEnemyStateModel) model;
            foreach (var enemy in _model.Enemies)
            {
                enemy.Selected += HandleActorSelected;
                enemy.Deselected += HandleActorDeselected;
            }
        }

        public void Clear()
        {
            foreach (var enemy in _model.Enemies)
            {
                enemy.Selected -= HandleActorSelected;
                enemy.Deselected -= HandleActorDeselected ;
            }
        }
        
        private void HandleActorSelected(object sender, BattleActor actor)
        {
            enemyWindowView.gameObject.SetActive(true);
            
            var actorName = actor.Actor.name;
            var health = actor.Health;
            var maxHealth = actor.MaxHealth;
            
            enemyWindowView.SetContext(actorName, null, "");
            enemyWindowView.SetHealth(health, maxHealth);
}
        
        private void HandleActorDeselected(object sender, BattleActor actor)
        {
            enemyWindowView.gameObject.SetActive(false);
        }
    }
}
