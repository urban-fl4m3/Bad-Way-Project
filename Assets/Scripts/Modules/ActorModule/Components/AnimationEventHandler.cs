using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class AnimationEventHandler :MonoBehaviour, IActorComponent
    {
        private readonly Dictionary<string, EventHandler> _eventsDictionary
            = new Dictionary<string, EventHandler>();
        
        public void Initialize(TypeContainer container)
        {
            container.Add<AnimationEventHandler>(this);
        }
        
        //Raising by animator unity editor
        public void RaiseEvent(string key)
        {
            var isExists = _eventsDictionary.TryGetValue(key, out var handler);

            if (isExists)
            {
                handler?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AddEvent(string key, EventHandler handler)
        {
            if (!_eventsDictionary.ContainsKey(key))
            {
                _eventsDictionary.Add(key, null);
            }

            _eventsDictionary[key] += handler;
        }
    }
}