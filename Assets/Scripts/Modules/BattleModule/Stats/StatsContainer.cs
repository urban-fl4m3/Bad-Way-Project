using System.Collections.Generic;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;

namespace Modules.BattleModule.Stats
{
    public class StatsContainer
    {
        public StatsContainer(IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IReadOnlyCollection<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats)
        {
            
        }
    }
}