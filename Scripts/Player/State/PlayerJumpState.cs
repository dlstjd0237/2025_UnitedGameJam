using BIS.Animators;
using BIS.Entities;
using GMS;
using System;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerJumpState : PlayerAirState
    {
        public PlayerJumpState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Vector2 jumpPower = new Vector2(0, _player.EntityStat.jumpPower.GetValue()); //todo : Stat기반으로 진행
            //CISSoundManager.Instance.PlaySound("Jump");
            _player.DecreaseJumpCount();
            _mover.StopImmediately(true);
            _mover.AddForceToEntity(jumpPower);
            _mover.OnMovement += HandleMovement;
        }

        private void HandleMovement(Vector2 dir)
        {
            if (dir.y < 0)
                _player.ChangeState("FALL");
        }


        public override void Exit()
        {
            _mover.OnMovement -= HandleMovement;

            base.Exit();
        }

    }
}
