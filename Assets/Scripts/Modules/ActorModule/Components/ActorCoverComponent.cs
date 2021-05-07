using System.Collections;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorCoverComponent: BaseActorComponent<ActorCoverComponent>
    {
        private Vector3 _coverOffset;

        public void CheckDistanceToCover()
        {
            var tr = transform;
            var ray = new Ray(tr.position, tr.forward);
            var collider = GetComponent<ActorCollisionComponent>().Collider as CapsuleCollider;
            
            if (Physics.Raycast(ray, out var hit, 2f))
            {
                _coverOffset = transform.position - hit.point;
                _coverOffset -= _coverOffset.normalized * collider.radius / 2;
                
                StartCoroutine(GetCover());
            }
        }

        private IEnumerator GetCover()
        {
            var currentPosition = transform.position - _coverOffset;
            while (transform.position!=currentPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPosition,
                    Time.deltaTime * 2);
                yield return null;
            }

            transform.eulerAngles += Vector3.up * 180;
        }
    }
}