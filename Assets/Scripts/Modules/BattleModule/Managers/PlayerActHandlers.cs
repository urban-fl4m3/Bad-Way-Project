using System;
using System.Linq;
using Common.Commands;
using Modules.ActorModule.Components;
using Modules.BattleModule.Helpers;
using Modules.CameraModule.Components;
using Modules.GridModule.Args;
using UnityEngine;

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
            IsActive.Value = IsActorActive(Actors[ActiveUnit]);

            var follower = new FlyFollower(nextPlayer.Actor.transform);
            _cameraController.GameCamera.GetActorComponent<SmoothFollowerComponent>().SetFollower(follower);
        }

        public void HandleMovementClicked(object sender, EventArgs e)
        {
            ActorAttack.Value = null;
            
            var battleActor = Actors[ActiveUnit];

            _grid.SetStateToken((int)BattlePlayerGridStates.WaitingForMove);
            _grid.HighlightRelativeCells(battleActor.Placement, 5, Color.white);
            
            var follower = new FlyFollower(Actors[ActiveUnit].Actor.transform);
            _cameraController.GameCamera.GetActorComponent<SmoothFollowerComponent>().SetFollower(follower);
            
        }

        public void HandleAttackClicked(object sender, EventArgs e)
        {
            var enemyActor = OnOppositeActors().ToList();
            var player = Actors[ActiveUnit].Actor.Transform;
            var actorTarget = Actors[ActiveUnit].Actor.GetActorComponent<ActorTargetComponent>();
            
            var enemyTransforms =
                (from battleActor in enemyActor
                    orderby Vector3.Distance(player.position, battleActor.Actor.transform.position)
                    select battleActor.Actor.transform).ToList();

            _grid.SetStateToken((int)BattlePlayerGridStates.WaitingForAttack);
            _grid.HighlightCells(enemyActor.Select(x => x.Placement), Color.red);

            var follower = new ThirdPersonFollower(enemyTransforms,
                actorTarget.ThirdPersonCamera, Actors[ActiveUnit].Actor.transform);
            _cameraController.GameCamera.GetActorComponent<SmoothFollowerComponent>().SetFollower(follower);
            
            ActorAttack.Value = Actors[ActiveUnit];
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