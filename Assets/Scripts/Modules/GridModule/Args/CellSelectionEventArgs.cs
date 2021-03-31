using System;

namespace Modules.GridModule.Args
{
    public class CellSelectionEventArgs : EventArgs
    {
        public readonly int Row;
        public readonly int Column;

        public CellSelectionEventArgs(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}