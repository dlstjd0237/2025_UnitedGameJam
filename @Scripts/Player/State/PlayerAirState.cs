using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Inputs;
using System;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerAirState : EntityState
    {
        protected EntityMover _mover;
        protected PlayerInputSO _inputSO;
        protected Player _player;
        public PlayerAirState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
        
        }

        public override void Enter()
        {
            base.Enter();
            _inputSO = _player.PlayerInput;
            _mover = _player.GetCompo<EntityMover>(); _mover.SetMovementMultiplier(0.6f);
            _inputSO.JumpEvent += HandleAirJumpEvent;
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.Movement.x;
            if (Mathf.Abs(xInput) > 0)
            {
                _mover.SetMovement(xInput);
            }

            

        }


        public override void Exit()
        {
            _mover.SetMovementMultiplier(1.0f);
            _inputSO.JumpEvent -= HandleAirJumpEvent;
            //_mover.StopImmediately();
            base.Exit();
        }

        private void HandleAirJumpEvent()
        {
            if (_player.CanAirJump)
                _player.ChangeState("JUMP");
            ///이곳에 더블점프 기믹 넣어주기
        }
    }
}
