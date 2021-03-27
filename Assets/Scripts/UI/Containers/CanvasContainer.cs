using UnityEngine;

namespace UI.Containers
{
    [CreateAssetMenu(fileName = "New Canvas Container", menuName = "UI/Canvas Container")]
    public class CanvasContainer : ScriptableObject
    {
        [SerializeField] private Canvas _mainCanvas;

        public Canvas MainCanvas => _mainCanvas;
    }
}