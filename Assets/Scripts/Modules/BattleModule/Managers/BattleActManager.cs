using System;
using System.Collections.Generic;
using Modules.GridModule;
using Modules.TickModule;
using UnityEngine;

namespace Modules.BattleModule.Managers
{
    public delegate IReadOnlyList<BattleActor> GetOppositeActors();
    
    public abstract class BattleActManager
    {
        public EventHandler EndTurn;
        
        public IReadOnlyList<BattleActor> Actors => _actors;
        public List<BattleActor> DeadActors => _deadActors;
        public event GetOppositeActors OppositeActors;
        public EventHandler ActorDeath;

        public int ActiveUnit { get; protected set; }

        protected readonly ITickManager _tickManager;
        protected readonly GridController _grid;

        private readonly List<BattleActor> _actors;
        private List<BattleActor> _deadActors =new List<BattleActor>();
        protected List<BattleActor> _activeActors;
        protected BattleActManager(GridController grid, List<BattleActor> actors, ITickManager tickManager)
        {
            _grid = grid;
            _actors = actors;
            _tickManager = tickManager;

            foreach (var battleActor in _actors)
            {
                battleActor.ActorDeath += OnActorDead;
            }
        }

        public void Reset()
        {
            _actors.AddRange(_deadActors);
            _deadActors.Clear();
        }
        public void ActStart()
        {
            _activeActors = new List<BattleActor>();
            foreach (var vActor in _actors)
            {
                _activeActors.Add(vActor);
            }
            if(_activeActors.Count==0)
                return;
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
                ActEnd();
            }
        }

        public void OnActorDead(object sender,BattleActor actor)
        {
            _actors.Remove(actor);
            _deadActors.Add(actor);
            ActorDeath?.Invoke(this,EventArgs.Empty);
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