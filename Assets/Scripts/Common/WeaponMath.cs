using Modules.GridModule.Cells;
using Modules.GunModule;
using UnityEngine;

namespace Common.Commands
{
    public static class WeaponMath
    {
        public static WeaponInfo ActorWeapon { get; set; }
        
        public static int HitChance( Cell eCell, Cell pCell)
        {
            var enemyPosition = new Vector2(eCell.Column, eCell.Row);
            var playerPosition = new Vector2(pCell.Column, pCell.Row);
            var effectiveRange = ActorWeapon.EffectiveRange;
            var maxRange = ActorWeapon.MaxRange;
            

            var distance = Vector2.Distance(enemyPosition, playerPosition);
            
            if (maxRange < distance)
                return 0;

            if (distance <= effectiveRange)
                return 100;

            //var chance = (maxRange - effectiveRange) / (distance - effectiveRange);
            return 50;
        }
        
    }
}