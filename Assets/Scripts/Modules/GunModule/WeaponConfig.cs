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

        private Dictionary<string, WeaponInfo> _weaponsDict = new Dictionary<string, WeaponInfo>();
        
        public void OnBeforeSerialize()
        {
           
        }

        public void OnAfterDeserialize()
        {
            _weaponsDict = _weaponWithIds.ToDictionary(x => x.Id,
                x => x.weaponInfo);
        }

        public WeaponInfo LoadWeapon(string id)
        {
            return _weaponsDict[id];
        }
        
        [Serializable]
        private struct WeaponWithId
        {
            public string Id;
            public WeaponInfo weaponInfo;
        }
    }
}
