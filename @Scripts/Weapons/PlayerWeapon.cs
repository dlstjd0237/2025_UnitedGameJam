using UnityEngine;
using BIS.Init;
using BIS.Entities;
using System.Collections.Generic;
using BIS.Define;
using BIS.Weapons;
using BIS.Inputs;
using System;
using BIS.Core;
using UnityEngine.Events;
using System.Collections;

namespace BIS.Players
{
    public class PlayerWeapon : MonoBehaviour, IEntityComponentInit, IAfterInit
    {
        [SerializeField] private Transform _attackTrm;
        [SerializeField] private List<WeaponStatSO> _weaponStatSOList;
        [SerializeField] private SpriteRenderer _weaponSprite;
        [SerializeField] private UnityEvent<float, float, float> _valueChangeEvent;
        [SerializeField] private GameEventChannelSO _trapHitChannelSO;
        private Entity _entity;
        private Player _player;

        Dictionary<EWeaponType, WeaponStatSO> _weaponDictionary;

        private WeaponStateMachine _weaponStateMachine;
        private PlayerInputSO _inputSO;
        private Vector2 _direction = Vector2.zero;
        private float _attackDamageMultiplier = 1.5f;
        public float AttackDamageMultiplier => _attackDamageMultiplier;

        public UnityEvent OnAttackEvent;

        public event WeaponChangeEvent _weaponChangeEvent;
        private bool _isFall = false;

        public void Initalize(Entity entity)
        {
            _entity = entity;
            _player = entity as Player;
            _inputSO = _player.PlayerInput;
            _weaponDictionary = new Dictionary<EWeaponType, WeaponStatSO>();
            _attackDamageMultiplier = 1;
            StartCoroutine(Falling());


            _trapHitChannelSO.AddListener<HitEvent>(HandleHitEvent);
            _trapHitChannelSO.AddListener<FallEvent>(HandleFallEvent);
        }

        private void HandleFallEvent(FallEvent evt)
        {
            _isFall = evt.isFall;
        }

        private IEnumerator Falling()
        {
            var Wait = new WaitForSeconds(1);
            while (true)
            {
                if (_isFall == true)
                    SetAttackDamageMultiplier(Mathf.Min(_attackDamageMultiplier += 0.1f, 1.5f));
                yield return Wait;
            }
        }

        private void HandleHitEvent(HitEvent evt)
        {
            SetAttackDamageMultiplier(Mathf.Max(_attackDamageMultiplier -= 0.2f, 0.5f));
        }



        public void SetAttackDamageMultiplier(float value)
        {
            _attackDamageMultiplier = value;
            _valueChangeEvent?.Invoke(_attackDamageMultiplier, 1.5f, 0.5f);
        }
        public void ChangeWeapon(EWeaponType weaponType)
        {
            _weaponStateMachine.ChangeWeapon(weaponType);
            _weaponChangeEvent?.Invoke(weaponType);
            _weaponSprite.sprite = _weaponDictionary[weaponType].WeaponSprite;
        }

        private void Update()
        {
            _direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _weaponSprite.transform.position).normalized;

            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            _weaponSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }

        private void HandleAttackEvent()
        {
            _weaponStateMachine.Attack(_direction);
        }
        public void AfterInit()
        {

            for (int i = 0; i < _weaponStatSOList.Count; ++i)
            {
                _weaponDictionary.Add(_weaponStatSOList[i].WeaponType, _weaponStatSOList[i]);
            }
            _weaponStateMachine = new WeaponStateMachine(_player, _attackTrm, _weaponDictionary);

            _inputSO.AttackEvent += HandleAttackEvent;
            _inputSO.AttackEvent += AttackEvent;

            _inputSO.Weapon1Event += HandleWeapon1Event;
            _inputSO.Weapon2Event += HandleWeapon2Event;
            _inputSO.Weapon3Event += HandleWeapon3Event;
        }

        public void AttackEvent()
        {
            OnAttackEvent?.Invoke();
        }

        private void HandleWeapon3Event()
        {
            ChangeWeapon(EWeaponType.Rail);
        }

        private void HandleWeapon2Event()
        {
            ChangeWeapon(EWeaponType.Shotgun);
        }

        private void HandleWeapon1Event()
        {
            ChangeWeapon(EWeaponType.Rifle);
        }

        private void OnDisable()
        {
            _trapHitChannelSO.RemoveListener<HitEvent>(HandleHitEvent);

            _inputSO.AttackEvent -= HandleAttackEvent;
            _inputSO.AttackEvent -= AttackEvent;

            _inputSO.Weapon1Event -= HandleWeapon1Event;
            _inputSO.Weapon2Event -= HandleWeapon2Event;
            _inputSO.Weapon3Event -= HandleWeapon3Event;
        }

    }
}
