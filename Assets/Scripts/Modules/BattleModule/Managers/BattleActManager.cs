using System.Collections.Generic;
using Modules.GridModule;
using Modules.TickModule;

namespace Modules.BattleModule.Managers
{
    public delegate IReadOnlyList<BattleActor> GetOppositeActors();
    
    public abstract class BattleActManager
    {
        public IReadOnlyList<BattleActor> Actors => _actors;
        public event GetOppositeActors OppositeActors;
        public int ActiveUnit { get; protected set; }

        protected readonly ITickManager _tickManager;
        protected readonly GridController _grid;

        private readonly List<BattleActor> _actors;
        private List<BattleActor> _activeActors;

        protected BattleActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager)
        {
            _grid = grid;
            _actors = actors;
            _tickManager = tickManager;
        }

        public void ActStart()
        {
            _activeActors = new List<BattleActor>();
            foreach (var vActor in _actors)
            {
                _activeActors.Add(vActor);
            }
            OnActStart();
        }

        public void ActEnd()
        {
            OnActEnd();
        }

        protected void RemoveActiveActor(BattleActor actor)
        {
            _activeActors.Remove(actor);
            
            if (_activeActors.Count == 0)
            {
                //Next turn
                ActEnd();
            }
        }

        protected bool IsActorActive(BattleActor actor)
        {
            return _activeActors.Count != 0 && _activeActors.Contains(actor);
        }

        protected abstract void OnActStart();
        protected abstract void OnActEnd();

        protected IEnumerable<BattleActor> OnOppositeActors()
        {
          return OppositeActors?.Invoke();
        }
    }
}