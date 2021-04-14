using Modules.BattleModule.Helpers;
using Modules.BattleModule.Managers;
using Modules.CameraModule;
using Modules.GridModule;
using Modules.GridModule.Args;

namespace Modules.BattleModule
{
    public class BattleScene
    {
        public readonly GridController Grid;
        public readonly BattleActManager PlayerActManager;
        public readonly BattleActManager EnemyActManager;
        public readonly CameraController CameraController;

        public BattleScene(GridController grid,
            BattleActManager playerActManager, BattleActManager enemyActManager, CameraController cameraController)
        {
            Grid = grid;
            PlayerActManager = playerActManager;
            EnemyActManager = enemyActManager;
            CameraController = cameraController;

            Grid.CellSelected += HandleCellSelected;
        }

        private void HandleCellSelected(object sender, CellSelectionEventArgs e)
        {
            var state = e.StateToken;
            
            switch (state)
            {
                case (int) BattlePlayerGridStates.WaitingForMove:
                {
                    PlayerMove(e.Row, e.Column);
                    break;  
                }

                case (int) BattlePlayerGridStates.WaitingForAttack:
                {
                    PlayerAtack(e.Row, e.Column);
                    break;
                }
            }
        }

        private void PlayerMove(int row, int column)
        {
            var nowPlayerSelect = 0;

            var cell = Grid[row, column];
            PlayerActManager.Actors[nowPlayerSelect].Actor.Transform.position = cell.Component.transform.position;
            PlayerActManager.Actors[nowPlayerSelect].Placement = cell;
            Grid.RemoveCellHighlights();
        }

        private void PlayerAtack(int row, int column)
        {
            
        }

        public void StartBattle()
        {
            //Начало хода, ходят все юниты игрока последовательно
            PlayerActManager.ActStart();

            //Как только игрок подвигал всеми юнитами делаем это
            // PlayerActManager.ActEnd();
            //  EnemyActManager.ActStart();

            //Как только враг походил всеми юнитами делаем это и репит с 24 строчки
            //   EnemyActManager.ActEnd();
        }
    }
}