using System;

namespace Modules.GridModule.Args
{
    public class CellEventArgs : EventArgs
    {
        public readonly int Row;
        public readonly int Column;
        
        public CellEventArgs(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}