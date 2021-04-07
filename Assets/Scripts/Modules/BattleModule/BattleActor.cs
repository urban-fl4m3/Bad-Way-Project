using System;
using Modules.ActorModule;
using Modules.GridModule.Cells;

namespace Modules.BattleModule
{
    public class BattleActor
    {
        private readonly Actor _actor;

        public Actor Actor => _actor;

        public BattleActor(Actor actor)
        {
            _actor = actor;
        }

        public Cell Placement
        {
            get =>_placement ;
            set
            {
                if (_placement != null)
                {
                    _placement.IsEmpty = true;
                }
                _placement = value;
                _placement.IsEmpty = false;
            }
        }
        
        private Cell _placement;
    } 
}
