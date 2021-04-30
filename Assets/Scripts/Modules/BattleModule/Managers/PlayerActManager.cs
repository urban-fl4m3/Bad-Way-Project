﻿using System;
using System.Collections.Generic;
using Common;
using Modules.ActorModule.Components;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.TickModule;

namespace Modules.BattleModule.Managers
{
    public partial class PlayerActManager : BattleActManager
    {
        private readonly CameraController _cameraController;
        public readonly DynamicValue<bool> PlayerDoingAct = new DynamicValue<bool>(true);
        public PlayerActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager,
            CameraController cameraController) 
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
            PlayerDoingAct.Value = false;
            
            actorNavMesh.NavMeshAgent.enabled = true;
            selectedActor.Placement = cell;
            actorNavMesh.DestinationReach += OnDestinationReach;
            
            selectedActor.Animator.AnimateCovering(false);
            selectedActor.Animator.ChangeMovingState(true);

            actorNavMesh.SetNextDestination(cell.CellComponent.transform.position);
            
            RemoveActiveActor(selectedActor);
            
            _grid.RemoveCellHighlights();
        }
        
        void OnDestinationReach(object sender, EventArgs e)
        {
            var selectedActor = Actors[ActiveUnit];
            var actorNavMesh = selectedActor.Actor.GetActorComponent<ActorNavigation>();
            var covers = _grid.NearCover(selectedActor.Placement);
            
            selectedActor.Animator.ChangeMovingState(false);
            if (covers.Count > 0)
            {
                selectedActor.Actor.transform.eulerAngles = GridMath.RotateToCover(covers[0], selectedActor.Placement);
                selectedActor.Actor.GetActorComponent<ActorCollisionComponent>().CheckDistanceToCover();
                selectedActor.Animator.AnimateCovering(true);
                actorNavMesh.NavMeshAgent.enabled = false;
            }
            PlayerDoingAct.Value = true;
            actorNavMesh.DestinationReach -= OnDestinationReach;
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