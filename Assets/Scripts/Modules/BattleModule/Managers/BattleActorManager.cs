using System.Collections.Generic;

namespace Modules.BattleModule.Managers
{
    public abstract class BattleActorManager
    {
        protected readonly List<BattleActor> _actors;

        protected BattleActorManager(List<BattleActor> actors)
        {
            _actors = actors;
        }
    }
}