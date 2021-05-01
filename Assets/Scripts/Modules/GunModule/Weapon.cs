using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.GunModule
{
    [CreateAssetMenu(fileName = "New Weapon Cofig", menuName = "Weapon")]
    public class Weapon : ScriptableObject
    {
        public string Name;
        public WeaponTag gunTag;
        public WeaponClass gunClass;
        public GameObject WeaponMesh;
        public Sprite View;
        public int GunLevel = 1;
        public int Damage = 1;
        public int Ammo;
        public int Clip;
        public int ActionPointToAttack = 1;
        public int ActionPointToReload = 1;
        public float EffectiveRadius = 1;
        public float MaxRadius = 1;


        public enum WeaponTag
        {
            Short,
            Long,
            ShortBarreled,
            LongBarreled
        }

        public enum WeaponClass
        {
            Blade,
            Crushing,
            Automation,
            Pistols,
            Shotguns,
            Sniper
        }
    }
}