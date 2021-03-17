using System;
using UnityEngine;

namespace Modules.GridModule.Cells
{
    public class Cell
    {
        private readonly CellComponent _component;
        private readonly int _row;
        private readonly int _column;

        public Cell(CellComponent component, int row, int column)
        {
            _component = component;
            _row = row;
            _column = column;

            _component.MousePressed += HandleMousePressed;
        }

        private void HandleMousePressed(object sender, EventArgs e)
        {
            Debug.Log($"{_row}:{_column}");
        }

        public void Clear()
        {
            _component.MousePressed -= HandleMousePressed;
        }
    }
}