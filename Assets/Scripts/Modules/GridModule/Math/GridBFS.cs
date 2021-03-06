using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Modules.GridModule.Cells;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.GridModule.Math
{
    public class GridBFS
    {
        private readonly Cell[,] _grid;
        private List<Transform> _walls;
        private readonly int _rows;
        private readonly int _columns;

        public void AddWalls(List<Transform> walls)
        {
            _walls = walls;
        }
        public GridBFS(Cell[,] grid, int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _grid = grid;
        }

        private bool CheckNearWall(Cell a, Cell b)
        {
            var row = (b.Row - a.Row);
            var column = (b.Column - a.Column);

            var position = a.Component.transform.position;
            var positionToCheck = new Vector3(position.x + column, 0,position.z + row);
            foreach (var wall in _walls)
            {
                if (wall.position == positionToCheck)
                {
                    Debug.Log(a.Column+"/"+a.Row+"  "+b.Column+'/'+b.Row);
                    return true;
                }
            }

            return false;
        }
        public IEnumerable<Cell> Search(Cell cell, int steps)
        {

            var currentStep = 0;
            var toVisit = new List<Cell> {cell};
            var result = new List<Cell>();

            var alreadyVisited = new bool[_rows, _columns];
            alreadyVisited[cell.Row, cell.Column] = true;
            
            while (currentStep < steps)
            {
                var visited = new List<Cell>();

                foreach (var c in toVisit)
                {
                    ProcessCell(c ,c.Row, c.Column - 1,  alreadyVisited, visited);
                    ProcessCell(c, c.Row, c.Column + 1, alreadyVisited, visited);
                    ProcessCell(c, c.Row - 1, c.Column,  alreadyVisited, visited);
                    ProcessCell(c, c.Row + 1, c.Column,  alreadyVisited, visited);
                }
                
                toVisit.Clear();
                toVisit.AddRange(visited);
                result.AddRange(visited);
                
                currentStep++;
            }
            
            return result;
        }
        
        private void ProcessCell(Cell startCell ,int row, int column, bool[,] alreadyVisited, ICollection<Cell> visited)
        {
            
            if (column < 0 || column >= _columns ||  row < 0 || row >= _rows || alreadyVisited[row, column]
                || !_grid[row,column].IsEmpty)
            {
                return;
            }

            if (CheckNearWall(startCell, _grid[row, column]))
                return;

            alreadyVisited[row, column] = true;
            visited.Add(_grid[row, column]);
        }
    }
}