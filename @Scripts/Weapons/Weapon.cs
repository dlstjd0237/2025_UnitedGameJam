using BIS.Entities;
using UnityEngine;

namespace BIS.Weapons
{
    public abstract class Weapon
    {
        public float attackCoolTime;
        public int attackDamage;
        public float attackRebound;
        public float bulletLifeTime;
        public float bulletSpeed;
        protected Entity _entity;
        protected EntityMover _mover;
        public Weapon(Entity entity, WeaponStatSO _stat)
        {
            _entity = entity;
            _mover = entity.GetCompo<EntityMover>();
            attackCoolTime = _stat.WeaponAttackCoolTime;
            attackDamage = _stat.WeaponAttackDamage;
            attackRebound = _stat.WeaponRebound;
            bulletLifeTime = _stat.BulletLifeTime;
            bulletSpeed = _stat.BUlletSpeed;
        }

        public abstract void Attack(Transform attackTrm,Vector2 dir);



    }
}
