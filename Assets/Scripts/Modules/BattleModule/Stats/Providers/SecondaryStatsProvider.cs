﻿using System;
using UnityEngine;

namespace Modules.BattleModule.Stats.Providers
{
    [Serializable]
    public class SecondaryStatsProvider
    {
        [SerializeField] private StatAndUpgrades[] _secondaryStats;
    }
}