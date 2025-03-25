using GMS;
using System;
using TMPro;
using UnityEngine;
using BIS.Utility;
using BIS.Manager;

namespace BIS.UI
{
    public class CoinUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = Util.FindChild<TextMeshProUGUI>(gameObject, "Text");
        }

        private void OnEnable()
        {
            Managers.GameScene.CoinValueChangeEvent += HandleValueChange;
            HandleValueChange(Managers.GameScene.Coin);
        }

        private void HandleValueChange(int value)
        {
            _text.SetText($"{value} Gold");
        }

        private void OnDisable()
        {
            Managers.GameScene.CoinValueChangeEvent -= HandleValueChange;
        }
    }
}
