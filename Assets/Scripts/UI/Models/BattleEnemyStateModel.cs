using System.Collections.Generic;
using Modules.BattleModule;

namespace UI.Models
{
    public class BattleEnemyStateModel: IModel
    {
        public readonly List<BattleActor> Enemies;

        public BattleEnemyStateModel(List<BattleActor> enemies)
        {
            Enemies = enemies;
        }
    }
}