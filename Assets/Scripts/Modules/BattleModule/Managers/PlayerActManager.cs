﻿using System;
using System.Collections.Generic;
using Common;
using Common.Commands;
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
        public readonly DynamicValue<BattleActor> ActorAttack = new DynamicValue<BattleActor>(null);
        public readonly DynamicValue<bool> PlayerEndTurn = new DynamicValue<bool>(false);
        public EventHandler ActorEndTurn;
        
        public PlayerActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager,
            CameraController cameraController) 
            : base(grid, actors, tickManager)
        {
            _cameraController = cameraController;
        }

        protected override void OnActStart()
        {
            _cameraController.PointAtActor(Actors[ActiveUnit].Actor.Transform, Actors[ActiveUnit].Actor.ThirdPersonCamera);
            PlayerDoingAct.Value = IsActorActive(Actors[ActiveUnit]);
            WeaponMath.ActorWeapon = Actors[ActiveUnit]._weaponInfo;
            PlayerEndTurn.Value = false;
            _grid.CellPressed += HandleCellSelected;
        }

        protected override void OnActEnd()
        {
            _grid.CellPressed -= HandleCellSelected;
            PlayerEndTurn.Value = true;
            ActorEndTurn?.Invoke(this, null);
        }

        private void PlayerMove(int row, int column)
        {
            var cell = _grid[row, column];
            var selectedActor = Actors[ActiveUnit];
            var actorNavMesh = selectedActor.Actor.GetActorComponent<ActorNavigationComponent>();
            PlayerDoingAct.Value = false;
            
            actorNavMesh.NavMeshAgent.enabled = true;
            selectedActor.Placement = cell;
            actorNavMesh.DestinationReach += OnDestinationReach;
            
            selectedActor.Animator.AnimateCovering(false);
            selectedActor.Animator.ChangeMovingState(true);

            actorNavMesh.SetNextDestination(cell.CellComponent.transform.position);
            
            
            _grid.RemoveCellHighlights();
        }
        
        void OnDestinationReach(object sender, EventArgs e)
        {
            var selectedActor = Actors[ActiveUnit];
            var actorNavMesh = selectedActor.Actor.GetActorComponent<ActorNavigationComponent>();
            var covers = _grid.NearCover(selectedActor.Placement);
            
            selectedActor.Animator.ChangeMovingState(false);
            if (covers.Count > 0)
            {
                selectedActor.Actor.transform.eulerAngles = GridMath.RotateToCover(covers[0], selectedActor.Placement);
                selectedActor.Actor.GetActorComponent<ActorCollisionComponent>().CheckDistanceToCover();
                selectedActor.Animator.AnimateCovering(true);
                actorNavMesh.NavMeshAgent.enabled = false;
            }
            RemoveActiveActor(selectedActor);
            PlayerDoingAct.Value = IsActorActive(selectedActor);
            actorNavMesh.DestinationReach -= OnDestinationReach;
        }
        
        private void PlayerAttack(int row, int column)
        {
            if(WeaponMath.HitChance(Actors[ActiveUnit].Placement,_grid[row,column])==0)
                return;
            
            var selectedActor = Actors[ActiveUnit];

            foreach (var battleActor in OnOppositeActors())
            {
                if (battleActor.Placement == _grid[row, column])
                    battleActor.TakeDamage(WeaponMath.ActorWeapon.Damage);
            }
            selectedActor.Animator.AnimateShooting();
            
            RemoveActiveActor(selectedActor);
            
            _grid.RemoveCellHighlights();
        }
    }
}