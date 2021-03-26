using System;
using Modules.GridModule.Cells;

namespace Modules.GridModule.Models
{
    [Serializable]
    public struct GridDataModel
    {
        public CellComponent CellPrefab;
        public int Columns;
        public int Rows;
    }
}