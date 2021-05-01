using System.Numerics;
using Modules.BattleModule;
using Modules.GridModule.Cells;

namespace Modules.GridModule
{
    public class GridControllerForAI : GridController
    {
        public GridControllerForAI(int rows, int columns, Cell[,] cells) : base(rows, columns, cells)
        {

        }

        public Cell FindShortestDistance(BattleActor enemy, BattleActor actor)
        {
            var cells = _bfs.Search(enemy.Placement, 5);
            var actorPos = new Vector2(actor.Placement.Column, actor.Placement.Row);
            var nearestCell = enemy.Placement;
            var nearestDistance = 1000f;

            foreach (var variabCell in cells)
            {
                var pos = new Vector2(variabCell.Column, variabCell.Row);
                var distance = Vector2.Distance(pos, actorPos);
                if (distance < nearestDistance)
                {
                    nearestCell = variabCell;
                    nearestDistance = distance;
                }
            }
            return nearestCell;
        }
    }
}