using System;
using Modules.BattleModule.Managers;
using Modules.GridModule;
using Reset;

namespace Modules.BattleModule
{
    public class BattleScene
    {
        public readonly GridController Grid;
        public readonly BattleActManager PlayerActManager;
        public readonly BattleActManager EnemyActManager;
        public IRules DeathMatchRules { get; private set; }
        public IReset Reset { get; }

        public BattleScene(GridController grid, BattleActManager playerActManager, BattleActManager enemyActManager,
            IReset reset)
        {
            Grid = grid;
            PlayerActManager = playerActManager;
            EnemyActManager = enemyActManager;
            Reset = reset;

            PlayerActManager.EndTurn += OnActorEndTurn;
            EnemyActManager.EndTurn += OnEnemyEndTurn;
            
            playerActManager.OppositeActors += () => enemyActManager.Actors;
            enemyActManager.OppositeActors += () => playerActManager.Actors;
            
            playerActManager.ActorDeath += ToCheckRules;
            enemyActManager.ActorDeath += ToCheckRules;

            DeathMatchRules = new DeathMatchRules(this);
        }

        public void StartBattle()
        {
            PlayerActManager.ActStart();
        }

        private void OnActorEndTurn(object sender, EventArgs e)
        {
            EnemyActManager.ActStart();
        }
        
        private void OnEnemyEndTurn(object sender, EventArgs e)
        {
            PlayerActManager.ActStart();
        }
        
        private void ToCheckRules(object sender, EventArgs e)
        {
            DeathMatchRules.CheckRules();
        }
    }
}