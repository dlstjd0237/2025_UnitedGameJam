using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Manager;
using BIS.Players;
using UnityEngine;
using System.Collections;

namespace BIS.Enemys
{
    public class EnemyBatIdleState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        private float _moveMultiplier = 1;
        public EnemyBatIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _mover = _entity.GetCompo<EntityMover>();
        }


        public override void Enter()
        {
            base.Enter();
            _player = Managers.GameScene.Player;
            _entity.StartCoroutine(EntityMove());
        }

        public override void Update()
        {
            base.Update();
            Vector3 dir = (_player.transform.position - _entity.transform.position).normalized;
            _entity.transform.position += _entity.EntityStat.moveSpeed.GetValue() * dir * Time.deltaTime * _moveMultiplier;
        }
        private IEnumerator EntityMove()
        {
            while (true)
            {
                _moveMultiplier = 8;
                yield return new WaitForSeconds(0.1f);
                _moveMultiplier = 1;
                yield return new WaitForSeconds(3);
            }

        }


    }
}
