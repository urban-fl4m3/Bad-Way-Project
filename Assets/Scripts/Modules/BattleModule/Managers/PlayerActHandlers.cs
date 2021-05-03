using System;
using System.Linq;
using Common;
using Common.Commands;
using Modules.ActorModule.Components;
using Modules.BattleModule.Helpers;
using Modules.GridModule.Args;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Modules.BattleModule.Managers
{
    public partial class PlayerActManager
    {
        public void HandleSelectActor(object sender, int actorIndex)
        {
            ActiveUnit = actorIndex;
            
            var nextPlayer = Actors[actorIndex];
            WeaponMath.ActorWeapon = nextPlayer._weaponInfo;
            
            _grid.RemoveCellHighlights();
            
            ActorAttack.Value = null;
            PlayerDoingAct.Value = IsActorActive(Actors[ActiveUnit]);
            
            _cameraController.PointAtActor(nextPlayer.Actor.transform, nextPlayer.Actor.ThirdPersonCamera);
            _cameraController.SetAttackPos(false);
        }

        public void HandleMovementClicked(object sender, EventArgs e)
        {
            ActorAttack.Value = null;
            
            var battleActor = Actors[ActiveUnit];
            
            _grid.SetStateToken((int)BattlePlayerGridStates.WaitingForMove);
            _grid.HighlightRelativeCells(battleActor.Placement, 5, Color.white);
            
            _cameraController.SetAttackPos(false);
        }

        public void HandleAttackClicked(object sender, EventArgs e)
        {
            var enemyActor = OnOppositeActors();
            
            _grid.SetStateToken((int)BattlePlayerGridStates.WaitingForAttack);
            _grid.HighlightCells(enemyActor.Select(x => x.Placement), Color.red);
            
            ActorAttack.Value = Actors[ActiveUnit];
            
            _cameraController.SetAttackPos(true);
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