using System.Collections.Generic;

namespace Modules.BattleModule.Managers
{
    public class BattleActManager
    {
        private readonly List<BattleActor> _actors;
        private readonly IActCallbacks _actCallbacks;

        public  IReadOnlyList<BattleActor> Actors => _actors;

        public BattleActManager(List<BattleActor> actors, IActCallbacks actCallbacks)
        {
            _actors = actors;
            _actCallbacks = actCallbacks;
        }

        public void ActStart()
        {
            _actCallbacks.ActStart();
        }

        public void ActEnd()
        {
            _actCallbacks.ActEnd();
        }

        public void SetScene(BattleScene scene)
        {
            _actCallbacks.SetScene(scene);
        }
    }
}