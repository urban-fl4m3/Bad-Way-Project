using System;
using UnityEngine;

namespace Modules.CameraModule
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float Smooth;
        [SerializeField] private Vector3 offset;

        private Transform SelectedActor;
        private Vector3 smoothPosition;

        private void Start()
        {
            smoothPosition = transform.position;
        }

        private void LateUpdate()
        {
           smoothPosition = Vector3.Lerp(smoothPosition, SelectedActor.position,
                Smooth*Time.deltaTime);
            transform.position = smoothPosition+ offset;
        }

        public void PointAtActor(Transform actor)
        {
            SelectedActor = actor;
        }
    }
}