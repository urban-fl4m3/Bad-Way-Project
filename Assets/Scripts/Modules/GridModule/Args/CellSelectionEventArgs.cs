using System;

namespace Modules.GridModule.Args
{
    public class CellSelectionEventArgs : EventArgs
    {
        public readonly int Row;
        public readonly int Column;
        public readonly int StateToken;
        
        public CellSelectionEventArgs(int row, int column, int stateToken)
        {
            Row = row;
            Column = column;
            StateToken = stateToken;
        }

        public CellSelectionEventArgs(CellEventArgs cellArgs, int stateToken)
        {
            Row = cellArgs.Row;
            Column = cellArgs.Column;
            StateToken = stateToken;
        }
    }
}