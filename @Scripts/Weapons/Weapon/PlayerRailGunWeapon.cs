using BIS.Entities;
using BIS.ETC;
using BIS.Manager;
using BIS.Players;
using BIS.Pool;
using UnityEngine;

namespace BIS.Weapons
{
    public class PlayerRailGunWeapon : Weapon
    {

        public PlayerRailGunWeapon(Entity entity, WeaponStatSO _stat) : base(entity, _stat)
        {

        }

        public override void Attack(Transform attackTrm, Vector2 dir)
        {
            Managers.Camera.ShakeCamera(Vector2.one * 4, 7, 7, 0.25f);

            dir = dir.normalized;
            _mover.KnockBack(-dir * attackRebound, 0.3f);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, angle + 90);

            PoolManager.SpawnFromPool("RailSound", attackTrm.position).GetComponent<PlaySound>().Play();
            GameObject obj = PoolManager.SpawnFromPool("Rail", attackTrm.position, rot);
            float attackMultiplay = _entity.GetCompo<PlayerWeapon>().AttackDamageMultiplier;
            obj.GetComponent<RailWeapon>().SetMovement(dir, bulletSpeed, (float)attackDamage * attackMultiplay, bulletLifeTime);
        }


    }
}
