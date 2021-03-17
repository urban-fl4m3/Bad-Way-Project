using System;
using UnityEngine;

namespace Modules.GridModule.Cells
{
    public class CellComponent : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public event EventHandler MousePressed;
        
        public Vector3 Size => _meshRenderer.bounds.size;
        
        private void OnMouseDown()
        {
            MousePressed?.Invoke(this, EventArgs.Empty);    
        }
    }
}