using System;
using System.Collections.Generic;
using Common;
using Modules.ActorModule.Components;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.GridModule.Cells;
using Modules.TickModule;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public class EnemyActManager : BattleActManager
    {
        private CameraController _cameraController;
        public EventHandler EnemyEndTurn;

        private GridControllerForAI _gridController;

        public EnemyActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager,
            CameraController cameraController) 
            : base(grid, actors, tickManager)
        {
            _cameraController = cameraController;
            _gridController = grid as GridControllerForAI;
        }


        protected override void OnActStart()
        {
            NextTurn();
        }

        public void NextTurn()
        {
            if (_activeActors.Count > 0)
            {
                var nearestActor = NearestActor(_activeActors[0]);
                var cellToMove = _gridController.FindShortestDistance(_activeActors[0], nearestActor);
                EnemyMove(_activeActors[0], cellToMove);
            }
        }
        
        

        protected override void OnActEnd()
        {
            EnemyEndTurn?.Invoke(this, null);
        }

        
        private void EnemyMove(BattleActor enemy, Cell cell)
        {
            var actorNavMesh = enemy.Actor.GetActorComponent<ActorNavigation>();
            actorNavMesh.NavMeshAgent.enabled = true;
            actorNavMesh.DestinationReach += OnDestinationReach;
            
            _cameraController.PointAtActor(enemy.Actor.transform,null);

            enemy.Placement = cell;
            enemy.Animator.AnimateCovering(false);
            enemy.Animator.ChangeMovingState(true);

            actorNavMesh.SetNextDestination(cell.CellComponent.transform.position);

            void OnDestinationReach(object sender, EventArgs e)
            {

                var covers = _grid.NearCover(enemy.Placement);
            
                enemy.Animator.ChangeMovingState(false);
                if (covers.Count > 0)
                {
                    enemy.Actor.transform.eulerAngles = GridMath.RotateToCover(covers[0], enemy.Placement);
                    enemy.Actor.GetActorComponent<ActorCollisionComponent>().CheckDistanceToCover();
                    enemy.Animator.AnimateCovering(true);
                    actorNavMesh.NavMeshAgent.enabled = false;
                }
                NextTurn();
                actorNavMesh.DestinationReach -= OnDestinationReach;
            }
            
            RemoveActiveActor(enemy);
        }
        private void EnemyAttack()
        {
            
        }
        private BattleActor NearestActor(BattleActor enemy)
        {
            var nearestDistance = 1000f;
            var actor = enemy;

            foreach (var varyOppositeActor in OnOppositeActors())
            {
                var distance = Vector3.Distance(enemy.Actor.transform.position,
                    varyOppositeActor.Actor.transform.position);
                
                if (!(distance < nearestDistance)) continue;
                
                nearestDistance = distance;
                actor = varyOppositeActor;
            }

            return actor;
        }
        

    }
}