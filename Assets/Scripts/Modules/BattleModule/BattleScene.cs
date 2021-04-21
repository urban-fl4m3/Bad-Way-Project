using System;
using Modules.ActorModule.Components;
using Modules.BattleModule.Helpers;
using Modules.BattleModule.Managers;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.GridModule.Args;
using UI;
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
        public readonly BattlePlayerControlsView BattlePlayerControlsView;

        public BattleScene(GridController grid,
            BattleActManager playerActManager, BattleActManager enemyActManager, CameraController cameraController,
            BattlePlayerControlsView battlePlayerControlsView)
        {
            Grid = grid;
            PlayerActManager = playerActManager;
            EnemyActManager = enemyActManager;
            CameraController = cameraController;
            BattlePlayerControlsView = battlePlayerControlsView;

            Grid.CellSelected += HandleCellSelected;
            
            playerActManager.OppositeActors += () => enemyActManager.Actors;
            enemyActManager.OppositeActors += () => playerActManager.Actors;
        }

        private void HandleCellSelected(object sender, CellSelectionEventArgs e)
        {
            var state = e.StateToken;

            switch (state)
            {
                case (int) BattlePlayerGridStates.WaitingForMove:
                {
                    PlayerMove(e.Row, e.Column);
                    break;  
                }

                case (int) BattlePlayerGridStates.WaitingForAttack:
                {
                    PlayerAtack(e.Row, e.Column);
                    break;
                }
            }
        }

        private void PlayerMove(int row, int column)
        {
            var nowPlayerSelect = PlayerActManager.ActiveUnit;
            var cell = Grid[row, column];
            var actor = PlayerActManager.Actors[nowPlayerSelect].Actor;
            var actorNavMesh = actor.GetActorComponent<ActorNavigation>();
            var battleActor = PlayerActManager.Actors[nowPlayerSelect];
            
            actorNavMesh.DestinationReach += OnDestinationReach;
            battleActor.CharacterAnimator.ChangeMovingState(true);

            actorNavMesh.SetNextCell(cell);
            
            PlayerActManager.RemoveActiveActor(battleActor);
            PlayerActManager.Actors[nowPlayerSelect].Placement = cell;
            BattlePlayerControlsView.SetActiveAllButton(false);
            
            Grid.RemoveCellHighlights();
            
             void OnDestinationReach(object sender, EventArgs e)
             {
                 battleActor.CharacterAnimator.ChangeMovingState(false);
                 actorNavMesh.DestinationReach -= OnDestinationReach;
             }
        }

       
        private void PlayerAtack(int row, int column)
        {
            
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