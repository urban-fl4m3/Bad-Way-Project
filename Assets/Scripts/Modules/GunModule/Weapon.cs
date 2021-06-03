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
            float currentDamage = startDamage;
            int state = 0;
            
            var weaponTag = _weaponInfo.Tag;
            var weaponClass = _weaponInfo.CLass;
            
            foreach (var addiction in _weaponAddiction.addiction)
            {
                float damageDecrease = 0;
                float damageBonus = 0;

                foreach (var classAddiciton in addiction.WeaponClassAddicitons)
                {
                    if (classAddiciton.weaponClass == weaponClass)
                    {
                        damageDecrease += classAddiciton.baseDecrease;
                        damageBonus += classAddiciton.bonus;
                    }
                }
                foreach (var tagAddiciton in addiction.WeaponTagAddicitons)
                {
                    if (tagAddiciton.WeaponTag == weaponTag)
                    {
                        damageDecrease += tagAddiciton.baseDecrease;
                        damageBonus += tagAddiciton.bonus;
                    }
                }

                currentDamage += startDamage * ((damageBonus * _primaryUpgredes[state] - damageDecrease) / 100);
                state++;
            }
            
            return (int) currentDamage;
        }
    }
}