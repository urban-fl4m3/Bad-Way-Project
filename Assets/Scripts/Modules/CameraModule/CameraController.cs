using UnityEngine;

namespace Modules.CameraModule
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float SmoothX;
        [SerializeField] private float SmoothY;
        [SerializeField] private Vector3 offset;
    
        private Transform SelectedActor;
    
        private void FixedUpdate()
        {
            transform.position = SelectedActor.position + offset;
        }

        public void PointAtActor(Transform actor)
        {
            SelectedActor = actor;
        }
    }
}