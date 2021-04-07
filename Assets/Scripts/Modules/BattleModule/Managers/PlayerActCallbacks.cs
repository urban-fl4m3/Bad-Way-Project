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
        private readonly UIController _uiController;

        private BattleScene _scene;
        
        public PlayerActCallbacks(GridController grid, ITickManager tickManager, UIController uiController)
        {
            _grid = grid;
            _tickManager = tickManager;
            _uiController = uiController;
        }

        public void ActStart()
        {
            _uiController.MovementClicked += HandleMovementClicked;
            _uiController.AtackClicked += HandleAtackClicked;
          //  _uiController.SelectedClick += HandleSelectActor;
            _uiController.Show();
        }

        public void ActEnd()
        {
            _uiController.MovementClicked -= HandleMovementClicked;
            _uiController.AtackClicked -= HandleAtackClicked; 
           // _uiController.SelectedClick -= HandleSelectActor;
            _uiController.Hide();
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