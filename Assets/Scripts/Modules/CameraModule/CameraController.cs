using System;
using UnityEngine;

namespace Modules.CameraModule
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float Smooth;
        [SerializeField] private Vector3 offset;

        private Transform SelectedActor;
        private Vector3 smoothPositon;

        private void Start()
        {
            smoothPositon = transform.position;
        }

        private void LateUpdate()
        {
           smoothPositon = Vector3.Lerp(smoothPositon, SelectedActor.position,
                Smooth*Time.deltaTime);
            transform.position = smoothPositon+ offset;
        }

        public void PointAtActor(Transform actor)
        {
            SelectedActor = actor;
        }
    }
}