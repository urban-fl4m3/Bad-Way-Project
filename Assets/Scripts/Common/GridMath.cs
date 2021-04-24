using UnityEngine;
using Modules.GridModule.Cells;

namespace Common
{
    public static class GridMath
    {
        public static Vector3 RotateToCover(Cell coverCell, Cell playreCell)
        {
            var coverCellPos = new Vector2Int(coverCell.Column, coverCell.Row);
            var playerCellPos = new Vector2Int(playreCell.Column, playreCell.Row);

            var directon = coverCellPos - playerCellPos;

            switch (directon.x)
            {
                case 1:
                    return Vector3.up * 90;
                case -1:
                    return Vector3.up * 270;
            }

            if (directon.y == 1)
                return Vector3.up * 0;
            
            return Vector3.up * 180;

        }
    }
}