using BIS.Animators;
using BIS.Enemys;
using BIS.Entities;
using BIS.FSM;
using System;
using UnityEngine;
using static BIS.Define.Define;

namespace BIS.Enemys
{
    public class EnemyOneEyeIdleState : EntityState
    {
        private Enemy _enemy;
        private EntityMover _mover;
        private int _facingDirection = 1;


        public EnemyOneEyeIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as Enemy;
            _mover = entity.GetCompo<EntityMover>();
        }


        public override void Update()
        {
            base.Update();
            _mover.SetMovement(_facingDirection);
            if (_enemy.IsWallDetected() == true)
                _facingDirection = _facingDirection == 1 ? -1 : 1;
        }


    }
}
