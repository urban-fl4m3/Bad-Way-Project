using System;
using UnityEditor;

namespace Modules.BattleModule.Stats.Editor
{
    [CustomPropertyDrawer(typeof(PrimaryStatsProvider))]
    public class PrimaryStatsProviderPropertyDrawer : StatsProviderPropertyDrawer
    {
        protected override string StatLabelName => "Main Stats";
        protected override string StatsPropertyName => "_mainStats";
        protected override string[] StatNames => Enum.GetNames(typeof(MainStat));
    }
}