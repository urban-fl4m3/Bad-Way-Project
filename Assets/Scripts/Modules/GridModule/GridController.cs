using Modules.GridModule.Cells;
using Modules.GridModule.Math;
using UnityEngine;

namespace Modules.GridModule
{
    public class GridController
    {
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
        }

        public void HighlightRelativeCells(Cell cell, int steps)
        {
            var bfs = new GridBFS(this);
            var result = bfs.Search(cell, steps);

            foreach (var cellToHighlight in result)
            {
                cellToHighlight.Highlight();
            }
        }
    }
}