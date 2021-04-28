using System;
using System.Reflection;
using Modules.ActorModule;
using Modules.BattleModule;
using UI.Components;
using UI.Interface;
using UI.Models;
using UnityEngine;

namespace UI
{
    public class BattleEnemyStateView : MonoBehaviour, ICanvasView, IViewModel

    {
        [SerializeField] private EnemyWindowView enemyWindowView;
        public Canvas Canvas { get; set; }
        public GameObject GameObject { get; }

        private BattleEnemyStateModel _model;

        public void ResolveModel(IModel model)
        {
            _model =(BattleEnemyStateModel) model;
            foreach (var enemy in _model._enemies)
            {
                enemy.Actor.ActorSelect += HandlerActorSelected;
                enemy.Actor.ActorUnSelect +=HandlerActorUnSelected ;
            }

        }
        public void Clear()
        {
            foreach (var enemy in _model._enemies)
            {
                enemy.Actor.ActorSelect -= HandlerActorSelected;
                enemy.Actor.ActorUnSelect -=HandlerActorUnSelected ;
            }
            
        }
        private void HandlerActorSelected(object sender, BattleActor actor)
        {
            Debug.Log("ss");
            enemyWindowView.gameObject.SetActive(true);
            
            var name = actor.Actor.name;
            var health = actor.Stats.Health;
            var maxHealth = actor.Stats.MaxHealth;
            
            enemyWindowView.SetContext(name, null, "");
            enemyWindowView.SetHealth(health,maxHealth);
        }
        private void HandlerActorUnSelected(object sender, EventArgs e)
        {
            Debug.Log("a");
            enemyWindowView.gameObject.SetActive(false);
        }
        
        
    }
}
