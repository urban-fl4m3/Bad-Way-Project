using System;
using System.Collections.Generic;
using System.Linq;
using Modules.BattleModule.Helpers;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.TickModule;
using UI;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public class PlayerActManager : BattleActManager
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
            _battlePlayerControlsView.MovementClicked += HandleMovementClicked;
            _battlePlayerControlsView.AtackClicked += HandleAtackClicked;
            _battlePlayerControlsView.SelectedClick += HandleSelectActor;
            _battlePlayerControlsView.Show();
        }

        protected override void OnActEnd()
        {
            _battlePlayerControlsView.MovementClicked -= HandleMovementClicked;
            _battlePlayerControlsView.AtackClicked -= HandleAtackClicked; 
            _battlePlayerControlsView.SelectedClick -= HandleSelectActor;
            _battlePlayerControlsView.Hide();
        }
        private void HandleSelectActor(object sender, int actorIndex)
        {
            ActiveUnit = actorIndex;
            _grid.RemoveCellHighlights();
            var nextPlayer = Actors[actorIndex];
            _battlePlayerControlsView.SetActiveAllButton(IsActorActive(Actors[actorIndex]));
            _cameraController.PointAtActor(nextPlayer.Actor.transform);
        }

        private void HandleMovementClicked(object sender, EventArgs e)
        {
            var battleActor = Actors[ActiveUnit];
            _grid.SetStateToken((int)BattlePlayerGridStates.WaitingForMove);
            _grid.HighlightRelativeCells(battleActor.Placement, 5, Color.white);
        }

        private void HandleAtackClicked(object sender, EventArgs e)
        {
            var enemyActor = OnOppositeActors();
            
            _grid.SetStateToken((int)BattlePlayerGridStates.WaitingForAttack);
            _grid.HighlightCells(enemyActor.Select(x => x.Placement), Color.red);
        }
    }
}