using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Inputs;
using System;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerGroundState : EntityState
    {
        protected Player _player;
        protected PlayerInputSO _inputSO;
        protected EntityMover _mover;


        public PlayerGroundState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
            _inputSO = _player.PlayerInput;
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
            _inputSO.JumpEvent += HandleJumpEvent;

        }
        public override void Update()
        {
            base.Update();
            if (_mover.IsGroundDetected() == false && _mover.CanManualMove)
            {
                {
                    _player.ChangeState("FALL");
                }
            }
        }

        public override void Exit()
        {
            _inputSO.JumpEvent -= HandleJumpEvent;
            base.Exit();
        }

        private void HandleJumpEvent()
        {
            _player.ChangeState("JUMP");
        }
    }
}
