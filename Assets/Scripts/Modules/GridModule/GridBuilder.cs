using Modules.GridModule.Cells;
using Modules.GridModule.Data;
using UnityEngine;

namespace Modules.GridModule
{
    public class GridBuilder
    {
        private readonly Transform _gridParent;
        
        public GridBuilder(string parentName)
        {
            var parentGameObject = new GameObject(parentName);
            _gridParent = parentGameObject.transform;
        }
        
        public GridController Build(GridData data)
        {
            var cells = new Cell[data.Rows, data.Columns];
            
            for (var row = 0; row < data.Rows; row++)
            {
                for (var column = 0; column < data.Columns; column++)
                {
                    var position = new Vector3(
                        column * (data.CellPrefab.Size.z),
                        data.CellPrefab.transform.position.y,
                        row * (data.CellPrefab.Size.x));
                    
                    var cellInstance = Object.Instantiate(data.CellPrefab, position, 
                        Quaternion.Euler(new Vector3(90, 0 ,0)), _gridParent);
                    var cell = new Cell(cellInstance, row, column);
                    cells[row, column] = cell;
                }
            }

            var grid = new GridController(data.Rows, data.Columns, cells);
            return grid;
        }
    }
}