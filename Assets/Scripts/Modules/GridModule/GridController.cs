using System;
using Modules.GridModule.Args;
using Modules.GridModule.Cells;
using Modules.GridModule.Math;
using UnityEngine;

namespace Modules.GridModule
{
    public class GridController
    {
        public event EventHandler CellSelected;
        
        public readonly int Rows;
        public readonly int Columns;
        
        private readonly Cell[,] _cells;

        public Cell this[int row, int column] => _cells[row, column];
        public Cell this[Vector2Int cellIndices] => _cells[cellIndices.x, cellIndices.y];

        public GridController(int columns, int rows, Cell[,] cells)
        {
            Columns = columns;
            Rows = rows;
            _cells = cells;

            foreach (var cell in _cells)
            {
                cell.CellSelected += HandleCellSelected;
            }
        }

        public void HighlightRelativeCells(Cell cell, int steps)
        {
            UnHighlightGrid();
            var bfs = new GridBFS(this);
            Debug.Log(cell.Column+" "+ cell.Row);
            var result = bfs.Search(cell, steps);
            
            foreach (var cellToHighlight in result)
            {
<<<<<<< HEAD
                cellToHighlight.CellComponent.MeshCollider.enabled = true;
                cellToHighlight.Highlight(Color.red);
=======
                cellToHighlight.ChangeColor(Color.red);
>>>>>>> 97e7dd82909ba9e2d9350dcac718e166e7041c7d
            }
        }

        public void UnHighlightGrid()
        {
            foreach (var cell in _cells)
            {
<<<<<<< HEAD
                cell.CellComponent.MeshCollider.enabled = false;
                cell.DeHighLight();
=======
                cell.ChangeColor(Color.green);
>>>>>>> 97e7dd82909ba9e2d9350dcac718e166e7041c7d
            }
        }

        public void ClearGrid()
        {
            foreach (var cell in _cells)
            {
                cell.CellSelected -= HandleCellSelected;
            }
        }

        private void HandleCellSelected(object sender, CellSelectionEventArgs e)
        {
            CellSelected?.Invoke(this, e);
        }
    }
}