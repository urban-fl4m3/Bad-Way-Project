using System.Collections.Generic;
using Modules.GridModule;
using Modules.TickModule;
using UnityEngine;

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

        public BattleActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager)
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

        public void RemoveActiveActor(BattleActor actor)
        {
            Debug.Log("Remove " +actor.Actor.name);
            _activeActors.Remove(actor);
        }

        public bool IsActorActive(BattleActor actor)
        {
            if(_activeActors.Count==0)
                return false;
            return _activeActors.Contains(actor);
        }

        protected abstract void OnActStart();
        protected abstract void OnActEnd();

        protected IReadOnlyList<BattleActor> OnOppositeActors()
        {
          return OppositeActors?.Invoke();
        }
    }
}