using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.GunModule
{
    [CreateAssetMenu(menuName = "Create WeaponConfig", fileName = "WeaponConfig", order = 0)]
    public class WeaponConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<WeaponWithId> _weaponWithIds;

        private Dictionary<string, Weapon> _weaponsDict = new Dictionary<string, Weapon>();
        
        public void OnBeforeSerialize()
        {
           
        }

        public void OnAfterDeserialize()
        {
            _weaponsDict = _weaponWithIds.ToDictionary(x => x.Id, x => x._weapon);
        }

        public Weapon LoadWeapon(string id)
        {
            return _weaponsDict[id];
        }
        
        [Serializable]
        private struct WeaponWithId
        {
            public string Id;
            public Weapon _weapon;
        }

    }
}
