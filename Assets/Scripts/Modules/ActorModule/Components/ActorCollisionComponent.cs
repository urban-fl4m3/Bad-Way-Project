using System;
using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorCollisionComponent : MonoBehaviour, IActorComponent
    {
        public event EventHandler<Actor> ActorSelected;
        
        [SerializeField] private Collider _collider;
        public Collider Collider => _collider;
        
        public void Initialize(TypeContainer container)
        {
            container.Add<ActorCollisionComponent>(this);    
        }

        private void OnMouseDown()
        {
            ActorSelected?.Invoke(this,null);
        }
    }
}