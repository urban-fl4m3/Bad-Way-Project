using System;
using Modules.BattleModule;
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
            foreach (var enemy in _model._enemies)
            {
                enemy.Selected += HandlerActorSelected;
                enemy.Deselected += HandlerActorDeselected;
            }
        }

        public void Clear()
        {
            foreach (var enemy in _model._enemies)
            {
                enemy.Selected -= HandlerActorSelected;
                enemy.Deselected -=HandlerActorDeselected ;
            }
        }
        
        private void HandlerActorSelected(object sender, BattleActor actor)
        {
            enemyWindowView.gameObject.SetActive(true);
            
            var actorName = actor.Actor.name;
            var health = actor.Stats.Health;
            var maxHealth = actor.Stats.MaxHealth;
            
            enemyWindowView.SetContext(actorName, null, "");
            enemyWindowView.SetHealth(health,maxHealth);
        }
        
        private void HandlerActorDeselected(object sender, EventArgs e)
        {
            enemyWindowView.gameObject.SetActive(false);
        }
    }
}
