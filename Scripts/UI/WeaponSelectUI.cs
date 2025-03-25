using BIS.Inputs;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BIS.UI
{
    public class WeaponSelectUI : MonoBehaviour
    {
        [SerializeField] private List<Vector2> _weaponsPosList;
        [SerializeField] private PlayerInputSO _inputSO;


        private void OnEnable()
        {
            _inputSO.Weapon1Event += HandleWeapon1Event;
            _inputSO.Weapon2Event += HandleWeapon2Event;
            _inputSO.Weapon3Event += HandleWeapon3Event;
        }
        private void OnDisable()
        {
            _inputSO.Weapon1Event -= HandleWeapon1Event;
            _inputSO.Weapon2Event -= HandleWeapon2Event;
            _inputSO.Weapon3Event -= HandleWeapon3Event;
        }
        private void HandleWeapon3Event()
        {
            (transform as RectTransform).DOAnchorPos(_weaponsPosList[02],0.4f);
        }

        private void HandleWeapon2Event()
        {
            (transform as RectTransform).DOAnchorPos(_weaponsPosList[1], 0.4f);
        }

        private void HandleWeapon1Event()
        {
            (transform as RectTransform).DOAnchorPos(_weaponsPosList[0],0.4f);
        }
    }
}
