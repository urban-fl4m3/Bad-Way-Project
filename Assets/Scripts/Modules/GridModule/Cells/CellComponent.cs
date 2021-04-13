using System;
using UnityEngine;

namespace Modules.GridModule.Cells
{
    public class CellComponent : MonoBehaviour
    {
        public readonly int Tint = Shader.PropertyToID("_Tint");

        public MeshRenderer MeshRenderer => _meshRenderer;
        [SerializeField] private MeshRenderer _meshRenderer;

        public event EventHandler MousePressed;
        
        public Vector3 Size => _meshRenderer.bounds.size;

        public MeshCollider MeshCollider=>_meshCollider;
        [SerializeField] private MeshCollider _meshCollider;
        
        public void ResetMaterial()
        {
            _meshRenderer.sharedMaterial = new Material(_meshRenderer.material);
        }

        private void OnMouseDown()
        {
            MousePressed?.Invoke(this, EventArgs.Empty);    
        }
    }
}