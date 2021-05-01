using System.Collections.Generic;
using Common;
using Modules.GunModule;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorWeapon : MonoBehaviour, IActorComponent

    {
        [SerializeField] private Transform WeaponPosition;

        private Weapon _weapon;
        
        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
            Instantiate(_weapon.WeaponMesh, WeaponPosition.position, Quaternion.identity,WeaponPosition);
        }

        public void Initialize(TypeContainer container)
        {
            container.Add<ActorWeapon>(this);
        }
    }
}