using System;
using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.ActorModule.Components
{
    public class ActorCollisionComponent : MonoBehaviour, IActorComponent
    {
        public event EventHandler ActorSelected;
        public event EventHandler ActorUnSelected;
        
        [SerializeField] private Collider _collider;
        public Collider Collider => _collider;

        private Vector3 coverOffset;
        
        public void Initialize(TypeContainer container)
        {
            container.Add<ActorCollisionComponent>(this);    
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ActorSelected?.Invoke(this, null);
            }

            if (Input.GetMouseButtonUp(1))
            {
                ActorUnSelected?.Invoke(this,null);
            }
}

        private void OnMouseExit()
        {
            ActorUnSelected?.Invoke(this,null);
        }

        public void CheckDistanceToCover()
        {
            var ray = new Ray(transform.position,transform.forward);
            
            if (Physics.Raycast(ray, out var hit, 2f))
            {
                coverOffset = transform.position - hit.point;
                StartCoroutine(GetCover());
            }
        }

        private IEnumerator GetCover()
        {
            var currentPosition = transform.position - coverOffset;
            while (transform.position!=currentPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPosition, Time.deltaTime * 2);
                yield return null;
            }
        }
    }
}