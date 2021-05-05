using System;
using TMPro;
using UnityEngine;

namespace Modules.BattleModule
{
    public class DeathMatchRules: IRules
    {
        public event EventHandler<Rules> RulesComplite;
        
        private readonly BattleScene _battleScene;
        
        
        public DeathMatchRules(BattleScene battleScene)
        {
            _battleScene = battleScene;
        }

        public void CheckForAllAliveActor()
        {
            var playerActor = _battleScene.PlayerActManager.Actors;
            var enemyActor = _battleScene.EnemyActManager.Actors;

            if (playerActor.Count < 1)
            {
                RulesComplite?.Invoke(this, Rules.PlayerLose);
                return;
            }

            if (enemyActor.Count < 1)
            {
                RulesComplite?.Invoke(this, Rules.PlayerWin);
                return;
            }
            
            Debug.Log("Continue battle");
        }

        
    }
}