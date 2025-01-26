using BIS.Bullets;
using BIS.Entities;
using BIS.Manager;
using BIS.Players;
using BIS.Pool;
using UnityEngine;
using System.Collections;
using BIS.ETC;

namespace BIS.Weapons
{
    public class PlayerRifleWeapon : Weapon
    {
        public PlayerRifleWeapon(Entity entity, WeaponStatSO _stat) : base(entity, _stat)
        {
        }

        public override void Attack(Transform attackTrm, Vector2 dir)
        {
            Managers.Camera.ShakeCamera(Vector2.one * 4, 7, 7, 0.25f);

            dir = dir.normalized;
            _mover.KnockBack(-dir * attackRebound, 0.1f);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, 0, angle + 90);
            _entity.StartCoroutine(AttackCourtine(dir, attackTrm, rot));
        }

        private IEnumerator AttackCourtine(Vector2 dir, Transform attackTrm, Quaternion rot)
        {
            for (int i = 0; i < 3; ++i)
            {
                PoolManager.SpawnFromPool("RaifleSound", attackTrm.position).GetComponent<PlaySound>().Play();
                GameObject obj = PoolManager.SpawnFromPool("PlayerRifleBullet", attackTrm.position, rot);
                float attackMultiplay = _entity.GetCompo<PlayerWeapon>().AttackDamageMultiplier;
                obj.GetComponent<Bullet>().SetMovement(dir, bulletSpeed, (float)attackDamage * attackMultiplay, bulletLifeTime);
                yield return new WaitForSeconds(0.2f);
            }
        }


    }
}
