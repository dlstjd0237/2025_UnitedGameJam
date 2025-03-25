using BIS.Define;
using UnityEngine;

namespace BIS
{
    [CreateAssetMenu(menuName = "BIS/SO/WeaponStat")]
    public class WeaponStatSO : ScriptableObject
    {
        [SerializeField] private EWeaponType _weaponType; public EWeaponType WeaponType { get { return _weaponType; } }
        [SerializeField] private float _weaponAttackCoolTime; public float WeaponAttackCoolTime { get { return _weaponAttackCoolTime; } }
        [SerializeField] private int _weaponAttackDamage; public int WeaponAttackDamage { get { return _weaponAttackDamage; } }
        [SerializeField] private float _weaponRebound; public float WeaponRebound { get { return _weaponRebound; } }
        [SerializeField] private float _bulletLifeTime; public float BulletLifeTime { get { return _bulletLifeTime; } }
        [SerializeField] private float _bulletSpeed; public float BUlletSpeed { get { return _bulletSpeed; } }
        [SerializeField] private Sprite _weaponSprite; public Sprite WeaponSprite { get { return _weaponSprite; } }

    }
}
