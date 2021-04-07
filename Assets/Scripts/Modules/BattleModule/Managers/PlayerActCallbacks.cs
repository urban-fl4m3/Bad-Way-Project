using System;
using Modules.GridModule;
using Modules.TickModule;
using UI;

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
            _uiController.Show();
        }

        public void ActEnd()
        {
            _uiController.MovementClicked -= HandleMovementClicked;
            _uiController.Hide();
        }

        public void SetScene(BattleScene scene)
        {
            _scene = scene;
        }

        private void HandleMovementClicked(object sender, EventArgs e)
        {
            var battleActor = _scene.PlayerActManager.Actors[0];
            _grid.HighlightRelativeCells(battleActor.Placement, 5);

        }
    }
}