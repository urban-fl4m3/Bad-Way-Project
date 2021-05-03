using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.GridModule.Cells
{
    public class CellComponent : MonoBehaviour
    {
        public readonly int Tint = Shader.PropertyToID("_Tint");
        
        public event EventHandler MousePressed;
        public event EventHandler SelectCell;
        public event EventHandler DeselectCell;
        public MeshRenderer MeshRenderer => _meshRenderer;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private MeshCollider _meshCollider;
        public bool ActiveMeshCollider
        {
            set
            {
                if(!value)
                    DeselectCell?.Invoke(this,EventArgs.Empty);
                _meshCollider.enabled = value;
            }
        }

        public Vector3 Size => _meshRenderer.bounds.size;

        
        public void ResetMaterial()
        {
            _meshRenderer.sharedMaterial = new Material(_meshRenderer.material);
        }

        private void OnMouseDown()
        {
            MousePressed?.Invoke(this, EventArgs.Empty);    
        }

        private void OnMouseOver()
        {
            SelectCell?.Invoke(this, EventArgs.Empty);
        }

        private void OnMouseExit()
        {
            DeselectCell?.Invoke(this,EventArgs.Empty);
        }
    }
}