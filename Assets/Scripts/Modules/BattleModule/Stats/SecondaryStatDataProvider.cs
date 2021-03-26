using Modules.BattleModule.Stats.Providers;
using UnityEngine;

namespace Modules.BattleModule.Stats
{
    [CreateAssetMenu(fileName = "New Secondary Stat Data Provider", menuName = "Stats/Secondary Stat Data Provider")]
    public class SecondaryStatDataProvider : ScriptableObject
    {
        [SerializeField] private SecondaryStatsProvider secondaryStatsProvider;
    }
}