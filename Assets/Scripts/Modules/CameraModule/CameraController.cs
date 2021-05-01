using Unity.Mathematics;
using UnityEngine;

namespace Modules.CameraModule
{
    public class CameraController : MonoBehaviour
    {
        public delegate void OnCameraPositionChangeDelegate(Vector3 newPosition);
        public event OnCameraPositionChangeDelegate OnCameraPositionChange;

        [SerializeField] private float Smooth;
        [SerializeField] private Vector3 offset;

        private Transform _selectedActor;
        private Transform _thirdPlayerPosition;
        private Vector3 _smoothPosition;
        private Vector3 _myPosition;
        private Quaternion _myRotation;
        private bool isAttack;

        private void Start()
        {
            _smoothPosition = transform.position;
            _myPosition = transform.position;
            _myRotation = transform.rotation;
        }

        private void LateUpdate()
        {
            //A little krinzh
            /*if (isAttack && _thirdPlayerPosition != null)
            {
                _smoothPosition = Vector3.Lerp(_smoothPosition, _thirdPlayerPosition.position,
                    Time.deltaTime * Smooth );
                transform.rotation = Quaternion.Lerp(transform.rotation, _thirdPlayerPosition.rotation, Time.deltaTime * Smooth);
                transform.position = _smoothPosition;
            }
            else
            {*/
                _smoothPosition = Vector3.Lerp(_smoothPosition, _selectedActor.position,
                    Smooth * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, _myRotation, Time.deltaTime * Smooth);
                transform.position = _smoothPosition + offset;
            //}

            if (transform.position != _myPosition && OnCameraPositionChange != null)
            {
                _myPosition = transform.position;
                OnCameraPositionChange(_myPosition);
            }
        }

        public void PointAtActor(Transform actor, Transform cameraPos)
        {
            _selectedActor = actor;
            _thirdPlayerPosition = cameraPos;
        }

        public void SetAttackPos(bool a)
        {
            isAttack = a;
          //  _smoothPosition = transform.position;
        }
    }
}