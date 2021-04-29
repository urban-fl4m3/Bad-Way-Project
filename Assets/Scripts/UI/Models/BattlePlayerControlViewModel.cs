using System;
using System.Collections.Generic;
using Modules.ActorModule;

namespace UI.Models
{
    public class BattlePlayerControlViewModel : IModel
    {
        public readonly EventHandler MovementClicked;
        public readonly EventHandler AttackClicked;
        public readonly EventHandler<int> SelectedClick;
        
        public readonly List<ActorDataProvider> ActorDataProvider;

        public BattlePlayerControlViewModel(EventHandler movementClicked, EventHandler attackClicked,
            EventHandler<int> selectedClick, List<ActorDataProvider> actorDataProvider)
        {
            MovementClicked = movementClicked;
            AttackClicked = attackClicked;
            ActorDataProvider = actorDataProvider;
            SelectedClick = selectedClick;
        }
    }
}