using System;
using Modules.BattleModule.Stats.Models;
using UnityEngine;

namespace Modules.BattleModule.Stats.Providers
{
    [Serializable]
    public class SecondaryStatsProvider
    {
        [SerializeField] public StatAndUpgrades[] SecondaryStats;
    }
}