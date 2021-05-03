using System;
using Modules.GridModule.Cells;
using UnityEngine;

namespace Modules.GridModule.Models
{
    [Serializable]
    public struct GridDataModel
    {
        public CellComponent CellPrefab;
        public GameObject CellSelecter;
        public int Columns;
        public int Rows;
    }
}