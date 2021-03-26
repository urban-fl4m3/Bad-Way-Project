using Modules.ActorModule;

namespace Modules.BattleModule
{
    public class BattleActor
    {
        private readonly Actor _actor;

        public BattleActor(Actor actor)
        {
            _actor = actor;
        }
    }
}