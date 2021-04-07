﻿using System;
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

        private Color _valueColor;

        public bool IsEmpty { get; set; }
        public bool IsAction { get; set; }
        
        public Cell(CellComponent component, int row, int column)
        {
            Component = component;
            Row = row;
            Column = column;

            Component.ResetMaterial();
            Component.MousePressed += HandleMousePressed;

            _valueColor = getColor();
        }

        private void HandleMousePressed(object sender, EventArgs e)
        {
            CellSelected?.Invoke(this, new CellSelectionEventArgs(Row, Column));
        }

        public void Clear()
        {
            Component.MousePressed -= HandleMousePressed;
        }

<<<<<<< HEAD
        public void DeHighLight()
        {
            var tintKey = Component.Tint;
            var tint = Component.MeshRenderer.sharedMaterial.GetColor(tintKey);
            var alpha = tint.a;
            tint = _valueColor;
            tint.a = alpha;
            Component.MeshRenderer.sharedMaterial.SetColor(tintKey, tint);
           // Component.ResetMaterial();
        }

        public void Highlight(Color color)
=======
        public void ChangeColor(Color color)
>>>>>>> 97e7dd82909ba9e2d9350dcac718e166e7041c7d
        {
            var tintKey = Component.Tint;
            var tint = Component.MeshRenderer.sharedMaterial.GetColor(tintKey);
            var alpha = tint.a;
            tint = color;
            tint.a = alpha;
            Component.MeshRenderer.sharedMaterial.SetColor(tintKey, tint);
        }

        private Color getColor()
        {
            return Component.MeshRenderer.sharedMaterial.GetColor(Component.Tint);
        }
    }
}