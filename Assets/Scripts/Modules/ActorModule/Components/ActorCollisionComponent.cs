using System;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorCollisionComponent : BaseActorComponent<ActorCollisionComponent>
    {
        public event EventHandler Selected;
        public event EventHandler Deselected;
        
        [SerializeField] private CapsuleCollider _collider;
        private Vector3 _coverOffset;

        public Collider Collider => _collider;
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Selected?.Invoke(this, null);
            }

            if (Input.GetMouseButtonUp(1))
            {
                Deselected?.Invoke(this, null);
            }
        }

        private void OnMouseExit()
        {
            Deselected?.Invoke(this,null);
        }
    }
}