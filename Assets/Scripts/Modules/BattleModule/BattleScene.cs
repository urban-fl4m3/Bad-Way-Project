using System;
using Modules.BattleModule.Managers;
using Modules.CameraModule;
using Modules.GridModule;
using UnityEngine;

namespace Modules.BattleModule
{
    public class BattleScene
    {
        public readonly GridController Grid;
        public readonly BattleActManager PlayerActManager;
        public readonly BattleActManager EnemyActManager;
        public readonly CameraController CameraController;
        public readonly IRules DeathMatchRules;

        public BattleScene(GridController grid,
            BattleActManager playerActManager, BattleActManager enemyActManager, CameraController cameraController)
        {
            Grid = grid;
            PlayerActManager = playerActManager;
            EnemyActManager = enemyActManager;
            CameraController = cameraController;

            var pActManager = PlayerActManager as PlayerActManager;
            pActManager.ActorEndTurn += OnActorEndTurn;
            
            var eActManager = EnemyActManager as EnemyActManager;
            eActManager.EnemyEndTurn += OnEnemyEndTurn;
            
            playerActManager.OppositeActors += () => enemyActManager.Actors;
            enemyActManager.OppositeActors += () => playerActManager.Actors;

            playerActManager.ActorDeath += ToCheckRules;
            enemyActManager.ActorDeath += ToCheckRules;
            
            DeathMatchRules = new DeathMatchRules(this);
            DeathMatchRules.RulesComplete += OnRulesComplete;
            
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
        private void OnRulesComplete(object sender, Rules e)
        {
            Debug.Log(e);
        }
        private void ToCheckRules(object sender, EventArgs e)
        {
            DeathMatchRules.CheckRules();
        }
    }
}