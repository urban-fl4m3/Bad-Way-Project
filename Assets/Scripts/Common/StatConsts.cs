using System;
using Modules.BattleModule.Stats.Helpers;

namespace Common
{
    public static class StatConsts
    {
        public static string[] PrimaryStatNames => Enum.GetNames(typeof(PrimaryStat));
        public static int PrimaryStatsCount => PrimaryStatNames.Length;
        
        public static string[] SecondaryStatNames => Enum.GetNames(typeof(SecondaryStat));
        public static int SecondaryStatsCount => SecondaryStatNames.Length;
    }
}