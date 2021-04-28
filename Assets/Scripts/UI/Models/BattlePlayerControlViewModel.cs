using System;
using System.Collections.Generic;
using Modules.ActorModule;
using UnityEngine;

namespace UI.Models
{
    public class BattlePlayerControlViewModel : IModel
    {
        public EventHandler MovementClicked;
        public EventHandler AttackClicked;
        public EventHandler<Actor> ActorClick;
        public EventHandler<int> SelectedClick;
        public List<ActorDataProvider> ActorDataProvider;
        
        // public DynamicValue<bool> Abc { get; }

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