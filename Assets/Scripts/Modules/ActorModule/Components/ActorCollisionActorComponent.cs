using System;
using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorCollisionActorComponent : MonoBehaviour, IActorComponent
    {
        public event EventHandler<Actor> ActorSelect;
        [SerializeField] private Collider _collider;
        
        public void Initialize(TypeContainer container)
        {
            container.Add<ActorCollisionActorComponent>(this);    
        }

        private void OnMouseDown()
        {
            ActorSelect?.Invoke(this,null);
        }
    }
}