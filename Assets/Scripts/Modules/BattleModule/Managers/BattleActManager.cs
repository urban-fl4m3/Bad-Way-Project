using System.Collections.Generic;

namespace Modules.BattleModule.Managers
{
    public class BattleActManager
    {
        private readonly List<BattleActor> _actors;
        private readonly IActCallbacks _actCallbacks;

        public BattleActManager(List<BattleActor> actors, IActCallbacks actCallbacks)
        {
            _actors = actors;
            _actCallbacks = actCallbacks;
        }

        public void Act()
        {
            _actCallbacks.Act();
        }
    }
}