using System.Collections.Generic;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;
using Modules.BattleModule.Stats.Providers;
using UnityEngine;

namespace Modules.BattleModule.Stats
{
    [CreateAssetMenu(fileName = "New Secondary Stat Data Provider", menuName = "Stats/Secondary Stat Data Provider")]
    public class SecondaryStatsDataProvider : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private SecondaryStatsProvider _secondaryStatsProvider;

        private readonly Dictionary<SecondaryStat, StatAndUpgrades> _secondaryStats =
            new Dictionary<SecondaryStat, StatAndUpgrades>();
        
        public IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> SecondaryStats => _secondaryStats;
        
        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            _secondaryStats.Clear();
            
            var statsAndUpgrades = _secondaryStatsProvider.SecondaryStats;
            
            for (var i = 0; i < statsAndUpgrades.Length; i++)
            {
                var stat = (SecondaryStat)i;
                var value = statsAndUpgrades[i];
                _secondaryStats.Add(stat, value);
            }
        }
    }
}