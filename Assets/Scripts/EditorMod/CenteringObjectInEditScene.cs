using UnityEngine;

namespace EditorMod
{
    public class CenteringObjectInEditScene : MonoBehaviour
    {
        private Vector3 _position => transform.position;
        
        [SerializeField] private int _magnitudeValue = 1;
        
        public void OnValidate()
        {
            var position = new Vector3(
                (int) (_position.x/_magnitudeValue),
                (int) (_position.y/_magnitudeValue),
                (int) (_position.z/_magnitudeValue));
            
            transform.position = position*_magnitudeValue;
        }
    }
}
