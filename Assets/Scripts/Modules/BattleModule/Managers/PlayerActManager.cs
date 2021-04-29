using System;
using System.Collections.Generic;
using Common;
using Modules.ActorModule;
using Modules.ActorModule.Components;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.TickModule;

namespace Modules.BattleModule.Managers
{
    public partial class PlayerActManager : BattleActManager
    {
        private readonly CameraController _cameraController;
        
        public PlayerActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager,
            CameraController cameraController, List<ActorDataProvider> actorDataProvider) 
            : base(grid, actors, tickManager)
        {
            _cameraController = cameraController;
        }

        protected override void OnActStart()
        {
            _grid.CellSelected += HandleCellSelected;
        }

        protected override void OnActEnd()
        {
            _grid.CellSelected -= HandleCellSelected;
        }

        private void PlayerMove(int row, int column)
        {
            var cell = _grid[row, column];
            var selectedActor = Actors[ActiveUnit];
            var actorNavMesh = selectedActor.Actor.GetActorComponent<ActorNavigation>();
            var covers = _grid.NearCover(cell);
            
            actorNavMesh.NavMeshAgent.enabled = true;
            actorNavMesh.DestinationReach += OnDestinationReach;
            
            selectedActor.Animator.ChangeMovingState(true);

            actorNavMesh.SetNextDestination(cell.CellComponent.transform.position);
            
            RemoveActiveActor(selectedActor);
            selectedActor.Placement = cell;

            _grid.RemoveCellHighlights();
            
            void OnDestinationReach(object sender, EventArgs e)
            {
                selectedActor.Animator.ChangeMovingState(false);
                if (covers.Count > 0)
                {
                    selectedActor.Actor.transform.eulerAngles = GridMath.RotateToCover(covers[0], cell);
                    selectedActor.Actor.GetActorComponent<ActorCollisionComponent>().CheckDistanceToCover();
                    selectedActor.Animator.AnimateCovering(true);
                    actorNavMesh.NavMeshAgent.enabled = false;
                }

                actorNavMesh.DestinationReach -= OnDestinationReach;
            }
        }
        
        private void PlayerAttack(int row, int column)
        {
            var selectedActor = Actors[ActiveUnit];

            foreach (var battleActor in OnOppositeActors())
            {
                if (battleActor.Placement == _grid[row, column])
                    battleActor.TakeDamage(25);
            }
            selectedActor.Animator.AnimateShooting();
            
            RemoveActiveActor(selectedActor);
            
            _grid.RemoveCellHighlights();
        }
    }
}