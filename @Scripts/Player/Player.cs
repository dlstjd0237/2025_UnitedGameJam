using BIS.Core;
using BIS.Define;
using BIS.Entities;
using BIS.ETC;
using BIS.FSM;
using BIS.Inputs;
using BIS.Pool;
using BIS.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BIS.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [field: SerializeField] public GameEventChannelSO FallEventChannelSO { get; private set; }

        public List<StateSO> states;

        private StateMachine _stateMachine;

        public float CurrentJumpCount { get; private set; }
        public bool CanAirJump => CurrentJumpCount > 0;

        private float _dashCoolTime = 1.5f;
        private float _lastDashTime = 0;

        protected override void AfterInitalize()
        {
            base.AfterInitalize(); //모든 컴포넌트와 이벤트 구독 완료 상태
            _stateMachine = new StateMachine(states, this);
            PlayerInput.DashEvent += HandleDashEvent;
            GetCompo<EntityHealth>().OnDead += HandleDeadEvent;

            GetStatFromComponent();
        }
        private void OnDisable()
        {
            PlayerInput.DashEvent -= HandleDashEvent;
            GetCompo<EntityHealth>().OnDead -= HandleDeadEvent;
        }
        private void HandleDeadEvent()
        {
            SceneControlManager.FadeOut(() => SceneManager.LoadScene("DeadScene"));
        }


        private void GetStatFromComponent()
        {
            CurrentJumpCount = EntityStat.jumpCount.GetValue();
        }

        private void Start()
        {
            _stateMachine.Initalize("IDLE"); //IDLE상태로 시작
        }

        private void Update()
        {
            _stateMachine.UpdateFSM();
        }

        public void HandleDashEvent()
        {
            if (_lastDashTime + _dashCoolTime <= Time.time)
            {
                StartCoroutine(StartDashCourtine());
                _lastDashTime = Time.time;
            }
        }

        private IEnumerator StartDashCourtine()
        {
            PoolManager.SpawnFromPool("DashSound", transform.position).GetComponent<PlaySound>().Play();
            GetCompo<EntityMover>().SetMovementMultiplier(7);
            yield return new WaitForSeconds(0.05f);
            GetCompo<EntityMover>().SetMovementMultiplier(1);
        }

        public void ChangeState(string newStateName)
        {
            _stateMachine.ChangeState(newStateName);
        }


        public void DecreaseJumpCount() => CurrentJumpCount--;
        public void ResetJumpCount() => CurrentJumpCount = EntityStat.jumpCount.GetValue(); //todo : Stat으로 변경 예정
    }
}
