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
        private List<Cell> _cellsWithCover;
        
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

        public void FillBuildingCell(List<Transform> building)
        {
            _cellsWithCover = new List<Cell>();
            foreach (var build in building)
            {
                var position = build.transform.position;
                var pos = new Vector2Int((int)position.x, (int)position.z)/2;
                _cells[pos.y, pos.x].IsEmpty = false;
                _cellsWithCover.Add(_cells[pos.y, pos.x]);
            }
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

        public List<Cell> NearCover(Cell actorCell)
        {
            var covers=new List<Cell>();
            for (var index = 0; index < _cellsWithCover.Count; index++)
            {
                var cell = _cellsWithCover[index];

                var cellPos = new Vector2(cell.Row, cell.Column);
                var actorCellPos = new Vector2(actorCell.Row, actorCell.Column);
                
                if (Vector2.Distance(cellPos,actorCellPos)<=1)
                {
                    covers.Add(cell);
                }
            }
            return covers;
        }
    }
}