using System;
using System.Collections.Generic;
using Modules.GridModule.Args;
using Modules.GridModule.Cells;
using Modules.GridModule.Math;
using UnityEngine;

namespace Modules.GridModule
{
    public class GridController
    {
        public event EventHandler<CellSelectionEventArgs> CellSelected;
        
        public Cell this[int row, int column] => _cells[row, column];
        public Cell this[Vector2Int cellIndices] => _cells[cellIndices.x, cellIndices.y];
        
        private readonly Cell[,] _cells;
        private readonly GridBFS _bfs;

        private int _stateToken;
        
        public GridController(int rows, int columns, Cell[,] cells)
        {
            _cells = cells;

            foreach (var cell in _cells)
            {
               cell.CellSelected += HandleCellSelected;
            }

            _bfs = new GridBFS(_cells, rows, columns);
        }

        public void SetStateToken(int stateToken)
        {
            _stateToken = stateToken;
        }

        public void HighlightRelativeCells(Cell cell, int steps, Color color)
        {
            var result = _bfs.Search(cell, steps);
            HighlightCells(result, color);
        }

        public void HighlightCells(IEnumerable<Cell> cells, Color color)
        {
            RemoveCellHighlights();
            
            foreach (var cell in cells)
            {
                cell.CellComponent.MeshCollider.enabled = true;
                cell.CellSelected += HandleCellSelected;
                cell.Highlight(color);
            }
        }
       
        public void RemoveCellHighlights()
        {
            foreach (var cell in _cells)
            {
                cell.CellComponent.MeshCollider.enabled = false;
                cell.CellSelected -= HandleCellSelected;
                cell.Highlight();
            }
        }

        private void HandleCellSelected(object sender, CellEventArgs e)
        {
            CellSelected?.Invoke(this, new CellSelectionEventArgs(e, _stateToken));
        }
    }
}