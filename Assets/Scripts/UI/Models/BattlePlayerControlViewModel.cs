using System;
using System.Collections.Generic;
using Common;
using Modules.ActorModule;

namespace UI.Models
{
    public class BattlePlayerControlViewModel : IModel
    {
        public readonly EventHandler MovementClicked;
        public readonly EventHandler AttackClicked;
        public readonly EventHandler<int> SelectedClick;
        public readonly DynamicValue<bool> PlayerEndTurn;
        public readonly DynamicValue<bool> PlayerStanding;
        public readonly List<ActorDataProvider> ActorDataProvider;

        public BattlePlayerControlViewModel(EventHandler movementClicked, EventHandler attackClicked,
            EventHandler<int> selectedClick, List<ActorDataProvider> actorDataProvider, DynamicValue<bool> playerStanding,
            DynamicValue<bool> playerEndTurn)
        {
            MovementClicked = movementClicked;
            AttackClicked = attackClicked;
            ActorDataProvider = actorDataProvider;
            SelectedClick = selectedClick;
            PlayerStanding = playerStanding;
            PlayerEndTurn = playerEndTurn;
        }
    }
}