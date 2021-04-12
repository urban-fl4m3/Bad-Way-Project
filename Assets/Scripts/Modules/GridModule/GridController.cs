using System;
using System.Collections.Generic;
using Modules.BattleModule;
using Modules.BattleModule.Factories;
using Modules.GridModule.Args;
using Modules.GridModule.Cells;
using Modules.GridModule.Math;
using UnityEngine;

namespace Modules.GridModule
{
    public class GridController
    {
        public event EventHandler<CellSelectionEventArgs> MoveCell;
        public event EventHandler AtackCell;

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
               // cell.CellSelected += HandleCellSelected;
            }
        }

        public void HighlightRelativeCells(Cell cell, int steps)
        {
            UnHighlightGrid();
            var bfs = new GridBFS(this);
            var result = bfs.Search(cell, steps);
            
            foreach (var cellToHighlight in result)
            {
                cellToHighlight.CellComponent.MeshCollider.enabled = true;
                cellToHighlight.CellSelected += HandleCellMoveSelected;
                cellToHighlight.Highlight(Color.white);
            }
        }

        public void HighlightEnemyCells(IReadOnlyList<BattleActor> readOnlyList)
        {
            UnHighlightGrid();
            foreach (var Enemy in readOnlyList)
            {
                Enemy.Placement.CellComponent.MeshCollider.enabled = true;
                Enemy.Placement.CellSelected += HandleCellAtackSelected;
                Enemy.Placement.Highlight(Color.red);
            }
            
        }
       
        public void UnHighlightGrid()
        {
            foreach (var cell in _cells)
            {
                cell.CellComponent.MeshCollider.enabled = false;
                cell.CellSelected -= HandleCellSelected;
                cell.CellSelected -= HandleCellMoveSelected;
                cell.CellSelected -= HandleCellAtackSelected;
                cell.Highlight();
            }
        }
       
        private void HandleCellSelected(object sender, CellSelectionEventArgs e)
        {
            MoveCell?.Invoke(this, e);
            Debug.Log("tap");
        }
      
        private void HandleCellMoveSelected(object sender, CellSelectionEventArgs e)
        {
            MoveCell?.Invoke(this,e);
            Debug.Log("move here");
        }
      
        private void HandleCellAtackSelected(object sender, CellSelectionEventArgs e)
        {
            AtackCell?.Invoke(this,e);
            Debug.Log("atack this");
        }

       
    }
}