using System;
using System.Collections.Generic;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.TickModule;
using UI;

namespace Modules.BattleModule.Managers
{
    public partial class PlayerActManager : BattleActManager
    {
        private readonly BattlePlayerControlsView _battlePlayerControlsView;
        private readonly CameraController _cameraController;
        
        public PlayerActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager,
            BattlePlayerControlsView battlePlayerControlsView, CameraController cameraController) 
            : base(grid, actors, tickManager)
        {
            _battlePlayerControlsView = battlePlayerControlsView;
            _cameraController = cameraController;
        }

        protected override void OnActStart()
        {
            _grid.CellSelected += HandleCellSelected;
            
            _battlePlayerControlsView.MovementClicked += HandleMovementClicked;
            _battlePlayerControlsView.AtackClicked += HandleAttackClicked;
            _battlePlayerControlsView.SelectedClick += HandleSelectActor;
            _battlePlayerControlsView.Show();
        }

        protected override void OnActEnd()
        {
            _grid.CellSelected -= HandleCellSelected;
            
            _battlePlayerControlsView.MovementClicked -= HandleMovementClicked;
            _battlePlayerControlsView.AtackClicked -= HandleAttackClicked; 
            _battlePlayerControlsView.SelectedClick -= HandleSelectActor;
            _battlePlayerControlsView.Hide();
        }

        private void PlayerMove(int row, int column)
        {
            var cell = _grid[row, column];
            var selectedActor = Actors[ActiveUnit];
            var actorNavMesh = selectedActor.Actor.GetActorComponent<ActorNavigation>();
            
            actorNavMesh.DestinationReach += OnDestinationReach;
            selectedActor.Animator.ChangeMovingState(true);

            actorNavMesh.SetNextCell(cell);
            
            RemoveActiveActor(selectedActor);
            selectedActor.Placement = cell;
            
            UpdateControlView(selectedActor);
            
            _grid.RemoveCellHighlights();
            
            void OnDestinationReach(object sender, EventArgs e)
            {
                selectedActor.Animator.ChangeMovingState(false);
                actorNavMesh.DestinationReach -= OnDestinationReach;
            }
        }

        private void PlayerAttack(int row, int column)
        {
            
        }
        

        private void UpdateControlView(BattleActor actor)
        {
            var isActive = IsActorActive(actor);
            _battlePlayerControlsView.SetActiveAllButton(isActive);
        }
    }
}