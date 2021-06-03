using System;
using System.Collections.Generic;
using Modules.BattleModule.Stats.Helpers;
using UnityEngine;


namespace Modules.GunModule.Helpers
{
    [CreateAssetMenu(menuName = "WeaponAddiction", fileName = "Create", order = 0)]
    public class WeaponAddiction: ScriptableObject
    {
        public List<Addiction> addiction;
    }
    [Serializable]
    public struct Addiction
    {
        public PrimaryStat primaryStat;
        public List<WeaponClassAddiciton> WeaponClassAddicitons;
        public List<WeaponTagAddiciton> WeaponTagAddicitons;
    }
    [Serializable]
    public struct WeaponClassAddiciton
    {
        public WeaponClass weaponClass;
        public int baseDecrease;
        public int bonus;
    }
    [Serializable]
    public struct WeaponTagAddiciton
    {
        public WeaponTag WeaponTag;
        public int baseDecrease;
        public int bonus;
    }
}