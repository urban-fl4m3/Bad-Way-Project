using System;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Providers;
using UnityEditor;

namespace Modules.BattleModule.Stats.Editor
{
    [CustomPropertyDrawer(typeof(PrimaryStatsProvider))]
    public class PrimaryStatsProviderPropertyDrawer : StatsProviderPropertyDrawer
    {
        protected override string StatLabelName => "Main Stats";
        protected override string StatsPropertyName => "PrimaryStats";
        protected override string[] StatNames => Enum.GetNames(typeof(PrimaryStat));
    }
}