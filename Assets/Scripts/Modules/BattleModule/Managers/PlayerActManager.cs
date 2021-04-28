using System;
using System.Collections.Generic;
using Common;
using Modules.ActorModule;
using Modules.ActorModule.Components;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.GridModule.Cells;
using Modules.InitializationModule;
using Modules.TickModule;
using UI;
using UI.Factories;
using UI.Models;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public partial class PlayerActManager : BattleActManager
    {
        
        private readonly CameraController _cameraController;

        private BattlePlayerControlViewModel _model;
        private WindowFactory _windowFactory;
        private DynamicValue<bool> IsPlayerActing;
        
        public PlayerActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager,
            WindowFactory windowFactory, CameraController cameraController, List<ActorDataProvider> actorDataProvider) 
            : base(grid, actors, tickManager)
        {
            _windowFactory = windowFactory;
            _cameraController = cameraController;

            IsPlayerActing = new DynamicValue<bool>(true);
            
            _model = new BattlePlayerControlViewModel(HandleMovementClicked,HandleAttackClicked,HandleSelectActor,actorDataProvider);

            windowFactory.AddWindow("PlayerView", _model);

            IsPlayerActing = new DynamicValue<bool>(true);

            
        }

        protected override void OnActStart()
        {
            _grid.CellSelected += HandleCellSelected;

            IsPlayerActing.Value = true;
        }

        protected override void OnActEnd()
        {
            _grid.CellSelected -= HandleCellSelected;

            IsPlayerActing.Value = false;
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
            
            UpdateControlView(selectedActor);
            
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
            UpdateControlView(selectedActor);
            
            _grid.RemoveCellHighlights();
        }
        

        private void UpdateControlView(BattleActor actor)
        {
            var isActive = IsActorActive(actor);
            
            if(!isActive)
                Debug.Log("End " +actor.Actor.name+" step");
            //_windowFactory.HideWindow("PlayerView", isActive);
        }
    }
}