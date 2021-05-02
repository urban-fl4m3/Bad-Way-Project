using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class WeaponPlaceholderComponent : BaseActorComponent<WeaponPlaceholderComponent>
    {
        [SerializeField] private Transform _weaponPlaceholder;
        
        private GameObject _weaponObject;

        public void SetWeapon(GameObject weaponPrefab)
        {
            _weaponObject = Instantiate(weaponPrefab, _weaponPlaceholder.position, Quaternion.identity, _weaponPlaceholder);
        }
    }
}