using System.Collections.Generic;
using UnityEngine;
using BIS.Define;
using System;
using BIS.Entities;
using BIS.Players;

namespace BIS.Weapons
{
    public class WeaponStateMachine
    {
        private Weapon _currentWeapon;
        public Weapon CurrentWeapon { get; private set; }

        private float lastAttackTime = 0;
        private Transform _attackTrm;
        private Dictionary<EWeaponType, Weapon> _weaponDictionary;
        private Entity _entity;

        public WeaponStateMachine(Entity entity, Transform attackTrm, Dictionary<EWeaponType, WeaponStatSO> statSODictionary)
        {
            _entity = entity;
            _attackTrm = attackTrm;
            _weaponDictionary = new Dictionary<EWeaponType, Weapon>();

            PlayerRifleWeapon _rigle = new PlayerRifleWeapon(entity, statSODictionary[EWeaponType.Rifle]);
            PlayerShotgunWeapon _shotGun = new PlayerShotgunWeapon(entity, statSODictionary[EWeaponType.Shotgun]);
            PlayerRailGunWeapon _railGun = new PlayerRailGunWeapon(entity, statSODictionary[EWeaponType.Rail]);
            _weaponDictionary.Add(EWeaponType.Rifle, _rigle);
            _weaponDictionary.Add(EWeaponType.Shotgun, _shotGun);
            _weaponDictionary.Add(EWeaponType.Rail, _railGun);

            _currentWeapon = _weaponDictionary[0];
        }

        public void Attack(Vector2 dir)
        {
            if (lastAttackTime + _currentWeapon.attackCoolTime < Time.time)
            {
                _currentWeapon.Attack(_attackTrm, dir);
                lastAttackTime = Time.time;
            }
        }

        public void ChangeWeapon(EWeaponType weaponType)
        {
            _currentWeapon = _weaponDictionary[weaponType];
        }
    }
}
