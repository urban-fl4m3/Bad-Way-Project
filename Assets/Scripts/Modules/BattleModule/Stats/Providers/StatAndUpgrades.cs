using System;
using System.Collections.Generic;
using Modules.BattleModule.Stats.Models;
using UnityEngine;

namespace Modules.BattleModule.Stats.Providers
{
    [Serializable]
    public struct StatAndUpgrades
    {
        [SerializeField] public int Value;
        [SerializeField] public List<PrimaryUpgrades> UpgradeList;
    }
}