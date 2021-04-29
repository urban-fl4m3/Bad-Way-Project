using UnityEngine;

namespace Modules.CameraModule
{
    public class CameraController : MonoBehaviour
    {
        public delegate void OnCameraPositionChangeDelegate(Vector3 newPosition);
        public event OnCameraPositionChangeDelegate OnCameraPositionChange;

        [SerializeField] private float Smooth;
        [SerializeField] private Vector3 offset;

        private Transform SelectedActor;
        private Vector3 smoothPosition;
        private Vector3 myPosition;

        private void Start()
        {
            smoothPosition = transform.position;
            myPosition = transform.position;
        }

        private void LateUpdate()
        {
            smoothPosition = Vector3.Lerp(smoothPosition, SelectedActor.position,
                Smooth * Time.deltaTime);
            transform.position = smoothPosition + offset;

            if (transform.position != myPosition && OnCameraPositionChange != null)
            {
                myPosition = transform.position;
                OnCameraPositionChange(myPosition);
            }
        }

        public void PointAtActor(Transform actor)
        {
            SelectedActor = actor;
        }
    }
}