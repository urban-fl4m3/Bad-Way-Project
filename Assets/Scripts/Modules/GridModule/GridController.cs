using Modules.GridModule.Cells;

namespace Modules.GridModule
{
    public class GridController
    {
        private readonly int _columns;
        private readonly int _rows;
        private readonly Cell[,] _cells;

        public Cell this[int row, int column] => _cells[row, column];

        public GridController(int columns, int rows, Cell[,] cells)
        {
            _columns = columns;
            _rows = rows;
            _cells = cells;
        }
    }
}