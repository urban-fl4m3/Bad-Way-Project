using System.Collections.Generic;
using System.Linq;
using Modules.GunModule.Helpers;

namespace Modules.GunModule
{
    public class Weapon
    {
        public WeaponInfo _weaponInfo;
        private List<int> _primaryUpgredes;
        private WeaponAddiction _weaponAddiction;

        public int Damage;
        public int Range;
        
        
        public Weapon(WeaponInfo weaponInfo, IEnumerable<int> primaryUpgrades, WeaponAddiction weaponAddiction)
        {
            _weaponInfo = weaponInfo;
            _primaryUpgredes = primaryUpgrades.ToList();
            _weaponAddiction = weaponAddiction;
            Damage = MathDamage();
        }

        private int MathDamage()
        {
            float startDamage = _weaponInfo.Damage;
            var currentDamage = startDamage;
            var state = 0;
            
            var weaponTag = _weaponInfo.Tag;
            var weaponClass = _weaponInfo.CLass;
            
            foreach (var addiction in _weaponAddiction.addiction)
            {
                float damageDecrease = 0;
                float damageBonus = 0;

                foreach (var classAddiction in addiction.WeaponClassAddicitons.Where(classAddiction => classAddiction.weaponClass == weaponClass))
                {
                    damageDecrease += classAddiction.baseDecrease;
                    damageBonus += classAddiction.bonus;
                }
                foreach (var tagAddiction in addiction.WeaponTagAddicitons.Where(tagAddiction => tagAddiction.WeaponTag == weaponTag))
                {
                    damageDecrease += tagAddiction.baseDecrease;
                    damageBonus += tagAddiction.bonus;
                }

                currentDamage += startDamage * ((damageBonus * _primaryUpgredes[state] - damageDecrease) / 100);
                state++;
            }
            
            return (int) currentDamage;
        }
    }
}