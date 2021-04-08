using System;
using Modules.GridModule;
using Modules.TickModule;
using UI;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public class PlayerActCallbacks : IActCallbacks
    { 
        private readonly GridController _grid;
        private readonly ITickManager _tickManager;
        private readonly BattlePlayerControlsView _battlePlayerControlsView;

        private BattleScene _scene;
        
        public PlayerActCallbacks(GridController grid, ITickManager tickManager,
            BattlePlayerControlsView battlePlayerControlsView)
        {
            _grid = grid;
            _tickManager = tickManager;
            _battlePlayerControlsView = battlePlayerControlsView;
            _battlePlayerControlsView.Initialize(2);
        }

        public void ActStart()
        {
            _battlePlayerControlsView.MovementClicked += HandleMovementClicked;
            _battlePlayerControlsView.AtackClicked += HandleAtackClicked;
          //  _uiController.SelectedClick += HandleSelectActor;
            _battlePlayerControlsView.Show();
        }

        public void ActEnd()
        {
            _battlePlayerControlsView.MovementClicked -= HandleMovementClicked;
            _battlePlayerControlsView.AtackClicked -= HandleAtackClicked; 
           // _uiController.SelectedClick -= HandleSelectActor;
            _battlePlayerControlsView.Hide();
            Debug.Log("Hide");
        }

        public void SetScene(BattleScene scene)
        {
            _scene = scene;
        }

        public void HandleSelectActor(object sender, EventArgs e)
        {
           // var nextPlayer = _scene.EnemyActManager.Actors[e];
           // _scene.CameraController.SelectNextActor(nextPlayer);
        }
        private void HandleMovementClicked(object sender, EventArgs e)
        {
            var battleActor = _scene.PlayerActManager.Actors[0];
            _grid.HighlightRelativeCells(battleActor.Placement, 5);

        }

        private void HandleAtackClicked(object sender, EventArgs e)
        {
            var enemyActor = _scene.EnemyActManager.Actors;
            _grid.HighlightEnemyCells(enemyActor);
        }
    }
}