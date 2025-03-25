using BIS.Animators;
using BIS.Bullets;
using BIS.Entities;
using BIS.FSM;
using BIS.Manager;
using BIS.Players;
using BIS.Pool;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemySkeletonIdleState : EntityState
    {
        private Player _player;

        private float _lastAttackTime;
        private float _attackColl = 3;
        public EnemySkeletonIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player = Managers.GameScene.Player;
        }

        public override void Update()
        {
            base.Update();

            if (_lastAttackTime + _attackColl < Time.time)
            {
                Vector3 dir = (_player.transform.position - _entity.transform.position).normalized;
                GameObject go = PoolManager.SpawnFromPool("EnemyBullet", _entity.transform.position);
                go.transform.GetComponent<Bullet>().SetMovement(dir, 30, _entity.EntityStat.attackDamage.GetValue(), 2.0f);
                _lastAttackTime = Time.time;
            }
        }

    }
}
