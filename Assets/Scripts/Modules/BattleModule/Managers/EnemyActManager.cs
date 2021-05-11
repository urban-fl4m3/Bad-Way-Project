using System;
using System.Collections.Generic;
using Common;
using Common.Commands;
using Modules.ActorModule.Components;
using Modules.CameraModule;
using Modules.CameraModule.Components;
using Modules.GridModule;
using Modules.GridModule.Cells;
using Modules.TickModule;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public class EnemyActManager : BattleActManager
    {
        private readonly CameraController _cameraController;
        private readonly GridController _gridController;

        public EnemyActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager,
            CameraController cameraController) 
            : base(grid, actors, tickManager)
        {
            _cameraController = cameraController;
            _gridController = grid;
        }


        protected override void OnActStart()
        {
            NextTurn();
        }

        private void NextTurn()
        {
            if (_activeActors.Count > 0)
            {
                WeaponMath.ActorWeapon = _activeActors[0]._weaponInfo;
                
                var nearestActor = NearestActor(_activeActors[0]);
                var actorPosition = new Vector2(nearestActor.Placement.Row, nearestActor.Placement.Column);
                var enemyPosition = new Vector2(_activeActors[0].Placement.Row, _activeActors[0].Placement.Column);
                var distance = Vector2.Distance(actorPosition, enemyPosition);
                
                if (distance <= WeaponMath.ActorWeapon.MaxRange)
                {
                    EnemyAttack(nearestActor,_activeActors[0]);
                }
                else
                {
                    var cellToMove = _gridController.FindShortestDistance(_activeActors[0].Placement,
                        nearestActor.Placement);
                    EnemyMove(_activeActors[0], cellToMove);
                }
            }
        }

        protected override void OnActEnd()
        {
            EndTurn?.Invoke(this, null);
        }


        private void EnemyMove(BattleActor enemy, Cell cell)
        {
            var actorNavMesh = enemy.Actor.GetActorComponent<ActorNavigationComponent>();
            actorNavMesh.NavMeshAgent.enabled = true;
            actorNavMesh.DestinationReach += OnDestinationReach;

            var follower = new FlyFollower(enemy.Actor.transform);
            _cameraController.GameCamera.GetActorComponent<SmoothFollowerComponent>().SetFollower(follower);

            enemy.Placement = cell;
            enemy.Animator.AnimateCovering(false);
            enemy.Animator.ChangeMovingState(true);

            actorNavMesh.SetNextDestination(cell.CellComponent.transform.position);

            void OnDestinationReach(object sender, EventArgs e)
            {

                var covers = _grid.FindAdjacentCells(enemy.Placement);
            
                enemy.Animator.ChangeMovingState(false);
                if (covers.Count > 0)
                {
                    enemy.Actor.transform.eulerAngles = GridMath.RotateToCover(covers[0], enemy.Placement);
                    enemy.Actor.GetActorComponent<ActorCoverComponent>().CheckDistanceToCover();
                    enemy.Animator.AnimateCovering(true);
                    actorNavMesh.NavMeshAgent.enabled = false;
                }

                RemoveActiveActor(enemy);
                NextTurn();
                actorNavMesh.DestinationReach -= OnDestinationReach;
            }   
        }

        private void EnemyAttack(BattleActor actor, BattleActor enemy)
        {
            var follower = (IFollower) new FlyFollower(actor.Actor.transform);
            _cameraController.GameCamera.GetActorComponent<SmoothFollowerComponent>().SetFollower(follower);
            
            actor.TakeDamage(WeaponMath.ActorWeapon.Damage);
            enemy.Animator.AnimateShooting();
            RemoveActiveActor(enemy);

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