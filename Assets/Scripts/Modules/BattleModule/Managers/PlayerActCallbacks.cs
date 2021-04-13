using System;
using System.Threading;
using Modules.BattleModule.Factories;
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
        private readonly BattleSceneFactory _battleSceneFactory;
        private BattleScene _scene;
        
        public int NowSelectedActor;

        public PlayerActCallbacks(GridController grid, ITickManager tickManager,
            BattlePlayerControlsView battlePlayerControlsView, BattleSceneFactory battleSceneFactory)
        {
            _grid = grid;
            _tickManager = tickManager;
            _battlePlayerControlsView = battlePlayerControlsView;
            _battleSceneFactory = battleSceneFactory;
            _battlePlayerControlsView.Initialize(battleSceneFactory.AvailableActorsProvider.AvailableActors,
                battleSceneFactory.AvailableBattleStatsProvider);
        }

        public void ActStart()
        {
            _battlePlayerControlsView.MovementClicked += HandleMovementClicked;
            _battlePlayerControlsView.AtackClicked += HandleAtackClicked;
           _battlePlayerControlsView.SelectedClick += HandleSelectActor;
            _battlePlayerControlsView.Show();
        }

        public void ActEnd()
        {
            _battlePlayerControlsView.MovementClicked -= HandleMovementClicked;
            _battlePlayerControlsView.AtackClicked -= HandleAtackClicked; 
           _battlePlayerControlsView.SelectedClick -= HandleSelectActor;
            _battlePlayerControlsView.Hide();
            Debug.Log("Hide");
        }

        public void SetScene(BattleScene scene)
        {
            _scene = scene;
        }

        public void HandleSelectActor(object sender,int i)
        {
            NowSelectedActor = i;
           var nextPlayer = _scene.PlayerActManager.Actors[i];
           _scene.CameraController.SelectNextActor(nextPlayer.Actor.transform);
        }
        private void HandleMovementClicked(object sender, EventArgs e)
        {
            var battleActor = _scene.PlayerActManager.Actors[NowSelectedActor];
            _grid.HighlightRelativeCells(battleActor.Placement, 5);
        }

        private void HandleAtackClicked(object sender, EventArgs e)
        {
            var enemyActor = _scene.EnemyActManager.Actors;
            _grid.HighlightEnemyCells(enemyActor);
        }
    }
}