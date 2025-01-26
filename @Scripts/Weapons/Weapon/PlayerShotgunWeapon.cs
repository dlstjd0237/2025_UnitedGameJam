using BIS.Bullets;
using BIS.Entities;
using BIS.ETC;
using BIS.Manager;
using BIS.Players;
using BIS.Pool;
using UnityEngine;

namespace BIS.Weapons
{
    public class PlayerShotgunWeapon : Weapon
    {
        public PlayerShotgunWeapon(Entity entity, WeaponStatSO _stat) : base(entity, _stat)
        {
        }

        public override void Attack(Transform attackTrm, Vector2 dir)
        {
            Managers.Camera.ShakeCamera(Vector2.one * 3,10, 10, 0.2f);
            dir = dir.normalized;
            _mover.KnockBack(-dir * attackRebound, 0.3f);
            int bulletCount = 5;      
            float spreadAngle = 30f;     
            float startAngle = -spreadAngle / 2;
            PoolManager.SpawnFromPool("SGSound", attackTrm.position).GetComponent<PlaySound>().Play();

            float attackMultiplay = _entity.GetCompo<PlayerWeapon>().AttackDamageMultiplier;
            for (int i = 0; i < bulletCount; i++)
            {
                // 각 탄환의 회전 각도 계산
                float angleOffset = startAngle + (spreadAngle / (bulletCount - 1)) * i;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angleOffset;
                Quaternion rot = Quaternion.Euler(0, 0, angle+90);

                // 탄환 생성
                GameObject obj = PoolManager.SpawnFromPool("PlayerRifleBullet", attackTrm.position, rot);
                Vector2 bulletDir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                obj.GetComponent<Bullet>().SetMovement(bulletDir, bulletSpeed, attackDamage* attackMultiplay, bulletLifeTime);
            }
        }
    }
}
