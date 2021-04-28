using System;
using Common;
using Modules.ActorModule;

namespace UI.Models
{
    public class BattlePlayerControlViewModel : IModel
    {
        public EventHandler MovementClicked;
        public EventHandler AttackClicked;
        public EventHandler<Actor> ActorClick;
        public EventHandler<int> SelectedClick;
        
        public DynamicValue<bool> Abc { get; }

        public BattlePlayerControlViewModel(DynamicValue<bool> abc)
        {
            Abc = abc;
        }
    }
}