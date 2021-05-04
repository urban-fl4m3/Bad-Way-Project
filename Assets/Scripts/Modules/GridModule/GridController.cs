using System;
using System.Collections.Generic;
using System.Linq;
using Modules.GridModule.Args;
using Modules.GridModule.Cells;
using Modules.GridModule.Math;
using UnityEngine;

namespace Modules.GridModule
{
    public class GridController
    {
        public event EventHandler<CellSelectionEventArgs> CellPressed;
        public event EventHandler<Cell> CellSelected;
        public event EventHandler CellDeselected;
        
        public Cell this[int row, int column] => _cells[row, column];
        public Cell this[Vector2Int cellIndices] => _cells[cellIndices.x, cellIndices.y];
        private List<Cell> _cellsWithCover;
        private readonly GameObject _cellSelector;
        
        private readonly Cell[,] _cells;
        private readonly GridBFS _bfs;

        private int _stateToken;

        public GridController(int rows, int columns, Cell[,] cells, GameObject cellSelector)
        {
            _cells = cells;
            _bfs = new GridBFS(_cells, rows, columns);
            _cellSelector = cellSelector;
        }

        public Cell FindShortestDistance(Cell enemy, Cell actor)
        {
            var cells = _bfs.Search(enemy, 5);
            var actorPos = new Vector2(actor.Column, actor.Row);
            var nearestCell = enemy;
            var nearestDistance = 1000f;

            foreach (var variabCell in cells)
            {
                var pos = new Vector2(variabCell.Column, variabCell.Row);
                var distance = Vector2.Distance(pos, actorPos);
                if (distance < nearestDistance)
                {
                    nearestCell = variabCell;
                    nearestDistance = distance;
                }
            }
            return nearestCell;
        }
        
        public void SetStateToken(int stateToken)
        {
            _stateToken = stateToken;
        }

        public void FillBuildingCell(IEnumerable<Transform> building)
        {
            _cellsWithCover = new List<Cell>();
            foreach (var pos in building
                .Select(build => build.transform.position)
                .Select(position => new Vector2Int((int)position.x, (int)position.z)/2))
            {
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
                cell.CellComponent.ActiveMeshCollider = true;
                cell.CellPressed += HandleCellPressed;
                cell.Highlight(color);
                
                
                if(color == Color.red)
                    continue;
                
                cell.CellSelected += HandleCellSelected;
                cell.CellDeselected += HandleCellDeselected;
            }
        }


        public void RemoveCellHighlights()
        {
            _cellSelector.SetActive(false);
            foreach (var cell in _cells)
            {
                cell.CellComponent.ActiveMeshCollider = false;
                cell.CellPressed -= HandleCellPressed;
                cell.CellSelected -= HandleCellSelected;
                cell.CellDeselected -= HandleCellDeselected;
                cell.Highlight();
            }
        }
        
        private void HandleCellSelected(object sender, Cell e)
        {
            CellSelected?.Invoke(this, e);
            _cellSelector.SetActive(true);
            _cellSelector.transform.position = e.CellComponent.transform.position;
        }

        private void HandleCellPressed(object sender, CellEventArgs e)
        {
            CellPressed?.Invoke(this, new CellSelectionEventArgs(e, _stateToken));
        }
        
        private void HandleCellDeselected(object sender, EventArgs e)
        {
            CellDeselected?.Invoke(this, EventArgs.Empty);
            _cellSelector.SetActive(false);
        }
        

        public List<Cell> FindAdjacentCells(Cell actorCell)
        {
            return (
                from cell in _cellsWithCover 
                let cellPos = new Vector2(cell.Row, cell.Column)
                let actorCellPos = new Vector2(actorCell.Row, actorCell.Column)
                where Vector2.Distance(cellPos, actorCellPos) <= 1 select cell).ToList();
        }
    }
}