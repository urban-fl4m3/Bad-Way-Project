using System;
using Modules.BattleModule.Stats.Providers;
using UnityEditor;

namespace Modules.BattleModule.Stats.Editor
{
    [CustomPropertyDrawer(typeof(SecondaryStatsProvider))]
    public class SecondaryStatsProviderPropertyDrawer : StatsProviderPropertyDrawer
    {
        protected override string StatLabelName => "Secondary Stats";
        protected override string StatsPropertyName => "_secondaryStats";
        protected override string[] StatNames => Enum.GetNames(typeof(SecondaryStat));
    }
}