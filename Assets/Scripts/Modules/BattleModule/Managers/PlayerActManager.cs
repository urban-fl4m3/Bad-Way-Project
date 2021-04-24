using System;
using System.Collections.Generic;
using Modules.ActorModule.Components;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.GridModule.Cells;
using Modules.TickModule;
using UI;
using UnityEngine;

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
            var covers = _grid.NearCover(cell);

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
                if (covers.Count > 0)
                {
                    selectedActor.Actor.transform.eulerAngles =  RotateToCover(covers[0], cell);
                    selectedActor.Actor.GetActorComponent<ActorCollisionComponent>().CheckDistanseToCover();
                    selectedActor.Animator.AnimateCovering(true);
                }

                actorNavMesh.DestinationReach -= OnDestinationReach;
            }
        }

        private Vector3 RotateToCover(Cell coverCell, Cell playreCell)
        {
            Vector2 coverCellPos = new Vector2(coverCell.Column, coverCell.Row);
            Vector2 playerCellPos = new Vector2(playreCell.Column, playreCell.Row);

            Vector2 directon = coverCellPos - playerCellPos;

            if (directon.x == 1)
                return Vector3.up * 90;
            if (directon.x == -1)
                return Vector3.up * 270;
            if (directon.y == 1)
                return Vector3.up * 0;
            
            return Vector3.up * 180;

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
            _battlePlayerControlsView.SetActiveAllButton(isActive);
        }
    }
}