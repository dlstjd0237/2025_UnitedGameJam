using BIS.FSM;
using BIS.Init;
using BIS.Players;
using BIS.Pool;
using DG.Tweening;
using System;
using UnityEngine;
using static BIS.Define.Define;

namespace BIS.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponentInit, IAfterInit
    {
        public event Action<Vector2> OnMovement;

        [Header("Collision detect")]
        [SerializeField] private Transform _groundCheckTrm;
        [SerializeField] private Vector2 _checkerSize;
        [SerializeField] private float _checkDistance;

        [SerializeField] private Transform _wallCheckTrm;
        [SerializeField] private Vector2 _wallCheckerSize;
        [SerializeField] private float _wallCheckDistance;


        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Transform _leftTrm;
        [SerializeField] private Transform _rightTrm;

        //[Header("Stats")]
        //[SerializeField] private StateSO _moveSpeedStat;

        private Rigidbody2D _rbCompo;
        private EntityRenderer _renderer;

        private float _movementX;
        private float _moveSpeedMultiplier, _origialGravity;

        [field: SerializeField] public bool CanManualMove { get; set; } = true;

        private Entity _entity;

        public void Initalize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();

            _origialGravity = _rbCompo.gravityScale;
            _moveSpeedMultiplier = 1.0f;
        }


        public void AfterInit()
        {
            _entity.GetCompo<EntityRenderer>().FacingDirectionChangeEvent += HandleFacingDirectionEvent;
            //_moveSpeedStat = _statCompo.GetStat(_moveSpeedStat);
            //_moveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            //_moveSpeed = _moveSpeedStat.Value; //초기화 한번은 필요함
        }

        private void HandleFacingDirectionEvent(int obj)
        {
            _wallCheckDistance = _wallCheckDistance * obj;

        }
        private void OnDisable()
        {
            _entity.GetCompo<EntityRenderer>().FacingDirectionChangeEvent -= HandleFacingDirectionEvent;

        }
        private void OnDestroy()
        {
            //_moveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
        }

        //private void HandleMoveSpeedChange(StatSO stat, float current, float prev) => _moveSpeed = current;

        private void FixedUpdate()
        {
            if (_rbCompo.linearVelocityY < -40)
            {
                _rbCompo.linearVelocityY = -40;
            }

            if (CanManualMove)
                _rbCompo.linearVelocityX = _movementX * _entity.EntityStat.moveSpeed.GetValue() * _moveSpeedMultiplier;

            OnMovement?.Invoke(_rbCompo.linearVelocity);

        }

        public void SetMovement(float xMovement)
        {
            _movementX = xMovement;
            _renderer.FlipController(xMovement);
        }

        public void StopImmediately(bool isYAxisToo = false)
        {
            if (_rbCompo == null)
                _entity.GetComponent<Rigidbody2D>();

            if (isYAxisToo)
                _rbCompo.linearVelocity = Vector2.zero;
            else
                _rbCompo.linearVelocityX = 0;

            _movementX = 0;
        }

        public void SetMovementMultiplier(float value) => _moveSpeedMultiplier = value;
        public void SetGravityMultiplier(float value) => _rbCompo.gravityScale = value;

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            if (_rbCompo == null)
                _entity.GetComponent<Rigidbody2D>();
            _rbCompo.AddForce(force, mode);
        }

        public void RandingSapwn() => _particleSystem.Play();

        public void RunEffect(bool isLeft)
        {
            if (isLeft == true)
                PoolManager.SpawnFromPool("LeftRun", _rightTrm.position);
            else
                PoolManager.SpawnFromPool("RightRun", _rightTrm.position);
        }



        #region KnockBack

        public void KnockBack(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately(true);
            AddForceToEntity(force);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }

        #endregion

        #region CheckCollision

        public virtual bool IsGroundDetected() => Physics2D.BoxCast(_groundCheckTrm.position, _checkerSize, 0, Vector2.down, _checkDistance, MLayerMask.WhatIsGround);
        public virtual bool IsWallDetected()
        {
            return Physics2D.BoxCast(_wallCheckTrm.position, _wallCheckerSize, 0, Vector2.right, _wallCheckDistance, MLayerMask.WhatIsWall);
        }
        #endregion

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (_groundCheckTrm != null)
            {
                Gizmos.color = Color.red;
                Vector3 offset = new Vector3(0, _checkDistance * 0.5f);
                Gizmos.DrawWireCube(_groundCheckTrm.position - offset, new Vector3(_checkerSize.x, _checkDistance, 1.0f));
            }


            if (_wallCheckTrm != null)
            {
                Vector3 wallOffset = new Vector3(_wallCheckDistance * 0.5f, 0);
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(_wallCheckTrm.position + wallOffset, new Vector3(_wallCheckerSize.x, _wallCheckerSize.y, 1.0f));
            }
        }



#endif
    }
}
