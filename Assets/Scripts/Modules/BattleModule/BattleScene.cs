using System;
using Modules.ActorModule.Components;
using Modules.BattleModule.Helpers;
using Modules.BattleModule.Managers;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.GridModule.Args;
using UI;
using UI.Factories;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.BattleModule
{
    public class BattleScene
    {
        public readonly GridController Grid;
        public readonly BattleActManager PlayerActManager;
        public readonly BattleActManager EnemyActManager;
        public readonly CameraController CameraController;
        public readonly WindowFactory _windowFactory;

        public BattleScene(GridController grid,
            BattleActManager playerActManager, BattleActManager enemyActManager, CameraController cameraController,
            WindowFactory windowFactory)
        {
            Grid = grid;
            PlayerActManager = playerActManager;
            EnemyActManager = enemyActManager;
            CameraController = cameraController;
            _windowFactory = windowFactory;

            
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
    }
}