using System.Collections.Generic;
using Modules.GridModule;
using Modules.TickModule;

namespace Modules.BattleModule.Managers
{
    public abstract class BattleActManager
    {
        public IReadOnlyList<BattleActor> Actors => _actors;

        protected readonly ITickManager _tickManager;
        protected readonly GridController _grid;

        private readonly List<BattleActor> _actors;

        public BattleActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager)
        {
            _grid = grid;
            _actors = actors;
            _tickManager = tickManager;
        }

        public void ActStart()
        {
            OnActStart();
        }

        public void ActEnd()
        {
            OnActEnd();
        }

        protected abstract void OnActStart();
        protected abstract void OnActEnd();
    }
}