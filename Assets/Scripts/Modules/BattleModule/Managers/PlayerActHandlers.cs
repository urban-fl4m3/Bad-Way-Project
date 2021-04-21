using System;
using System.Linq;
using Modules.BattleModule.Helpers;
using Modules.GridModule.Args;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public partial class PlayerActManager
    {
        private void HandleSelectActor(object sender, int actorIndex)
        {
            ActiveUnit = actorIndex;
            _grid.RemoveCellHighlights();
            var nextPlayer = Actors[actorIndex];
            _cameraController.PointAtActor(nextPlayer.Actor.transform);
            
            UpdateControlView(Actors[actorIndex]);
        }

        private void HandleMovementClicked(object sender, EventArgs e)
        {
            var battleActor = Actors[ActiveUnit];
            _grid.SetStateToken((int)BattlePlayerGridStates.WaitingForMove);
            _grid.HighlightRelativeCells(battleActor.Placement, 5, Color.white);
        }

        private void HandleAttackClicked(object sender, EventArgs e)
        {
            var enemyActor = OnOppositeActors();
            
            _grid.SetStateToken((int)BattlePlayerGridStates.WaitingForAttack);
            _grid.HighlightCells(enemyActor.Select(x => x.Placement), Color.red);
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
                    PlayerAttack(e.Row, e.Column);
                    break;
                }
            }
        }
    }
}