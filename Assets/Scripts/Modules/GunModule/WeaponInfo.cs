using Modules.GunModule.Helpers;
using UnityEngine;

namespace Modules.GunModule
{
    [CreateAssetMenu(fileName = "New Weapon Info", menuName = "Weapon Info")]
    public class WeaponInfo : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private WeaponTag _tag;
        [SerializeField] private WeaponClass _class;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _level = 1;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _ammo;
        [SerializeField] private int _clipSize;
        [SerializeField] private int _attackActionCost = 1;
        [SerializeField] private int _reloadActionCost = 1;
        [SerializeField] private int _effectiveRange = 1;
        [SerializeField] private int _maxRange = 1;

        public string Name => _name;
        public WeaponTag Tag => _tag;
        public WeaponClass CLass => _class;
        public GameObject Prefab => _prefab;
        public Sprite Icon => _icon;
        public int Level => _level;
        public int Damage => _damage;
        public int Ammo => _ammo;
        public int ClipSize => _clipSize;
        public int AttackActionCost => _attackActionCost;
        public int ReloadActionCost => _reloadActionCost;
        public int EffectiveRange => _effectiveRange;
        public int MaxRange => _maxRange;
    }
}