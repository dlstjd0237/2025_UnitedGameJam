using BIS.Entities;
using BIS.FSM;
using BIS.Manager;
using BIS.Players;
using GMS;
using System;
using System.Collections.Generic;
using UnityEngine;
using static BIS.Define.Define;

namespace BIS.Enemys
{
    public class Enemy : Entity
    {
        public List<StateSO> states;
        [SerializeField] private float _wallCheckDistance = 1f; // 벽을 감지하는 거리
        private StateMachine _stateMachine;
        private Player _player;
        protected override void AfterInitalize()
        {
            base.AfterInitalize(); //모든 컴포넌트와 이벤트 구독 완료 상태
            _stateMachine = new StateMachine(states, this);
        }


        private void Start()
        {
            _stateMachine.Initalize("IDLE"); //IDLE상태로 시작
            GetCompo<EntityHealth>().OnDead += HandleDeadEvent;
            _player = Managers.GameScene.Player;
        }
        private void OnDisable()
        {
            StopAllCoroutines();
            GetCompo<EntityHealth>().OnDead -= HandleDeadEvent;
        }

        private void HandleDeadEvent()
        {
            Managers.Camera.ShakeCamera(Vector2.one * 2, 2, 2, 0.2f);
            Managers.GameScene.AddCoint(UnityEngine.Random.Range(3, 9));
            gameObject.SetActive(false);
        }

        private void Update()
        {
            _stateMachine.UpdateFSM();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.TryGetComponent<Player>(out Player player))
                {
                    player.GetCompo<EntityHealth>().ApplyDamage(EntityStat.attackDamage.GetValue());
                }
            }
        }

        private void FixedUpdate()
        {
            if (_player.transform.position.y > transform.position.y)
            {   
                if (Vector2.Distance(_player.transform.position, transform.position) >= 30)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public bool IsWallDetected()
        {
            Vector2 origin = transform.position;
            Vector2 direction = new Vector2(GetCompo<EntityRenderer>().FacingDirection, 0); // 현재 이동 방향

            // Raycast를 이용해 벽 감지
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, _wallCheckDistance, MLayerMask.WhatIsGround);

            Debug.DrawRay(origin, direction * _wallCheckDistance, Color.red); // 디버그용 레이 표시
            return hit.collider != null;
        }

        public void ChangeState(string newStateName)
        {
            _stateMachine.ChangeState(newStateName);
        }

    }
}
