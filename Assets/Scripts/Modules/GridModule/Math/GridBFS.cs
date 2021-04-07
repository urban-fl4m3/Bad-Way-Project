using System.Collections.Generic;
using Modules.GridModule.Cells;
using UnityEditor.VersionControl;
using UnityEditorInternal;
using UnityEngine;

namespace Modules.GridModule.Math
{
    public class GridBFS
    {
        private readonly GridController _grid;
        
        public GridBFS(GridController grid)
        {
            _grid = grid;
        }
        
        public IEnumerable<Cell> Search(Cell cell, int steps)
        {
            var currentStep = 0;
            var toVisit = new List<Cell> {cell};
            var result = new List<Cell>();

            var alreadyVisited = new bool[_grid.Rows, _grid.Columns];
            alreadyVisited[cell.Row, cell.Column] = true;
            
            while (currentStep < steps)
            {
                var visited = new List<Cell>();

                foreach (var c in toVisit)
                {
                    ProcessCell(c.Column - 1, c.Row, alreadyVisited, visited);
                    ProcessCell(c.Column + 1, c.Row, alreadyVisited, visited);
                    ProcessCell(c.Column, c.Row - 1, alreadyVisited, visited);
                    ProcessCell(c.Column, c.Row + 1, alreadyVisited, visited);
                }

                foreach (var activeCell in toVisit)
                {
                    activeCell.IsAction = true;
                }
                
                toVisit.Clear();
                toVisit.AddRange(visited);
                result.AddRange(visited);
                
                currentStep++;
            }

            return result;
        }
        
        private void ProcessCell(int column, int row, bool[,] alreadyVisited, ICollection<Cell> visited)
        {
            if (column < 0 || column >= _grid.Columns ||  row < 0 || row >= _grid.Rows||
                alreadyVisited[column, row])
            {
                return;
            }
            
            visited.Add(_grid[column, row]);
            alreadyVisited[column, row] = true;
            
        }
    }
}