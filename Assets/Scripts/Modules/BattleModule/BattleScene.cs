using System;
using Modules.BattleModule.Managers;
using Modules.CameraModule;
using Modules.GridModule;

namespace Modules.BattleModule
{
    public class BattleScene
    {
        public readonly GridController Grid;
        public readonly BattleActManager PlayerActManager;
        public readonly BattleActManager EnemyActManager;
        public readonly CameraController CameraController;

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
        }

        


        public void StartBattle()
        {
            //Начало хода, ходят все юниты игрока последовательно
            PlayerActManager.ActStart();

            //Как только игрок подвигал всеми юнитами делаем это
            // PlayerActManager.ActEnd();
            //  EnemyActManager.ActStart();

            //Как только враг походил всеми юнитами делаем это и репит с 24 строчки
            //   EnemyActManager.ActEnd();
        }

        private void OnActorEndTurn(object sender, EventArgs e)
        {
            EnemyActManager.ActStart();
        }

        private void OnEnemyEndTurn(object sender, EventArgs e)
        {
            PlayerActManager.ActStart();
        }
    }
}