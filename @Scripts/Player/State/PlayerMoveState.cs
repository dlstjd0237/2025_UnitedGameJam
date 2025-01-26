using BIS.Animators;
using BIS.Entities;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.Movement.x;
            _mover.SetMovement(xInput);

            if (Mathf.Approximately(xInput, 0))
            {
                Debug.Log(_renderer.FacingDirection);
                if (_renderer.FacingDirection == 1)
                    _mover.RunEffect(false);
                else
                    _mover.RunEffect(true);
                _player.ChangeState("IDLE");
            }
        }

    }
}
