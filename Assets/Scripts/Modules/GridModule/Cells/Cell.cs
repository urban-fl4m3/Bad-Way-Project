using System;
using UnityEngine;

namespace Modules.GridModule.Cells
{
    public class Cell
    {
        public readonly CellComponent Component;
        
        private readonly int _row;
        private readonly int _column;

        public Cell(CellComponent component, int row, int column)
        {
            Component = component;
            _row = row;
            _column = column;

            Component.ResetMaterial();
            Component.MousePressed += HandleMousePressed;
        }

        private void HandleMousePressed(object sender, EventArgs e)
        {
            Debug.Log($"{_row}:{_column}");
        }

        public void Clear()
        {
            Component.MousePressed -= HandleMousePressed;
        }
    }
}