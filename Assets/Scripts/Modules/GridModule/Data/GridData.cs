using Modules.GridModule.Cells;
using UnityEngine;

namespace Modules.GridModule.Data
{
    [CreateAssetMenu(fileName = "New Grid Data", menuName = "Grid Module/Data")]
    public class GridData : ScriptableObject
    {
        [SerializeField] private CellComponent _cellPrefab;
        [SerializeField] private int _columns;
        [SerializeField] private int _rows;

        public CellComponent CellPrefab => _cellPrefab;
        public int Columns => _columns;
        public int Rows => _rows;
    }
}