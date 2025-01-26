using BIS.Animators;
using BIS.Core;
using BIS.Entities;
using GMS;
using System;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }


        public override void Enter()
        {
            base.Enter();
            _mover.OnMovement += HandleMovement;
            

            FallEvent evt = TrapHitEvent.PlayerFallEvent;
            evt.isFall = true;
            _player.FallEventChannelSO.RaiseEvent(evt);

        }

        private void HandleMovement(Vector2 dir)
        {
            if (dir.y > 0)
                _player.ChangeState("JUMP");
        }

        public override void Update()
        {
            base.Update();
            if (_mover.IsGroundDetected() == true)
            {
                _mover.RandingSapwn();
                _player.ResetJumpCount();
                _player.ChangeState("IDLE");
            }
        }

        public override void Exit()
        {
            _mover.OnMovement -= HandleMovement;
            FallEvent evt = TrapHitEvent.PlayerFallEvent;
            evt.isFall = false;
            _player.FallEventChannelSO.RaiseEvent(evt);
            base.Exit();
        }
    }
}
