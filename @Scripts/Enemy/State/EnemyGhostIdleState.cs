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
    public class EnemyGhostIdleState : EntityState
    {
        private Enemy _enemy;
        private Player _player;
        private EntityMover _mover;
        private float _lastAttackTime;
        private float _attackColl = 2;
        private int _facingDirection = 1;


        public EnemyGhostIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as Enemy;
            _mover = entity.GetCompo<EntityMover>();
        }
        public override void Enter()
        {
            base.Enter();
            _player = Managers.GameScene.Player;
            _lastAttackTime = Time.time;
        }
        public override void Update()
        {
            base.Update();
            _mover.SetMovement(_facingDirection);
            if (_enemy.IsWallDetected() == true)
                _facingDirection = _facingDirection == 1 ? -1 : 1;

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
