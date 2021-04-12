using System;
using Modules.GridModule.Args;
using UnityEngine;

namespace Modules.GridModule.Cells
{
    public class Cell
    {
        public event EventHandler<CellSelectionEventArgs> CellSelected;

        public readonly CellComponent Component;
        public CellComponent CellComponent => Component;
        
        public readonly int Row;
        public readonly int Column;

        public bool IsEmpty { get; set; }
        public bool IsAction { get; set; }

        private Color _valueColor;
        
        public Cell(CellComponent component, int row, int column)
        {
            Component = component;
            Row = row;
            Column = column;

            Component.ResetMaterial();
            Component.MousePressed += HandleMousePressed;

            _valueColor = GetColor();
        }

        private void HandleMousePressed(object sender, EventArgs e)
        {
            CellSelected?.Invoke(this, new CellSelectionEventArgs(Row, Column));
        }

        public void Clear()
        {
            Component.MousePressed -= HandleMousePressed;
        }

        public void Highlight()
        {
            Highlight(_valueColor);
        }

        public void Highlight(Color color)
        {
            var tintKey = Component.Tint;
            var tint = Component.MeshRenderer.sharedMaterial.GetColor(tintKey);
            var alpha = tint.a;
            tint = color;
            tint.a = alpha;
            Component.MeshRenderer.sharedMaterial.SetColor(tintKey, tint);
        }

        private Color GetColor()
        {
            return Component.MeshRenderer.sharedMaterial.GetColor(Component.Tint);
        }
    }
}