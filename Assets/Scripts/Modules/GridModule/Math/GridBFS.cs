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
                    ProcessCell(c.Row, c.Column - 1,  alreadyVisited, visited);
                    ProcessCell(c.Row, c.Column + 1, alreadyVisited, visited);
                    ProcessCell(c.Row - 1, c.Column,  alreadyVisited, visited);
                    ProcessCell(c.Row + 1, c.Column,  alreadyVisited, visited);
                }
                
                toVisit.Clear();
                toVisit.AddRange(visited);
                result.AddRange(visited);
                
                currentStep++;
            }
            
            return result;
        }
        
        private void ProcessCell(int row, int column, bool[,] alreadyVisited, ICollection<Cell> visited)
        {
            
            if (column < 0 || column >= _grid.Columns ||  row < 0 || row >= _grid.Rows ||
                alreadyVisited[row, column])
            {
                return;
            }
            
            alreadyVisited[row, column] = true;
            visited.Add(_grid[row, column]);
        }
    }
}