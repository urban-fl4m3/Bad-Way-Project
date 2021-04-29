﻿using System;
using System.Collections;
using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorCollisionComponent : MonoBehaviour, IActorComponent
    {
        public event EventHandler Selected;
        public event EventHandler Deselected;
        
        [SerializeField] private CapsuleCollider _collider;
        private Vector3 _coverOffset;
        
        public void Initialize(TypeContainer container)
        {
            container.Add<ActorCollisionComponent>(this);    
        }

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

        public void CheckDistanceToCover()
        {
            var tr = transform;
            var ray = new Ray(tr.position, tr.forward);

            if (Physics.Raycast(ray, out var hit, 2f))
            {
                _coverOffset = transform.position - hit.point;
                _coverOffset -= _coverOffset.normalized * _collider.radius / 2;
                
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