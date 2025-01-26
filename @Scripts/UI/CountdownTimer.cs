using BIS.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BIS.UI
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _systemChannelSO;
        private TextMeshProUGUI _timerText;  // 텍스트 UI를 할당할 변수
        private float _elapsedTime = 0f;  // 경과 시간
        private bool _timerRunning = false;  // 타이머 상태

        private void OnEnable()
        {

            _elapsedTime = PlayerPrefs.GetFloat("Time", 0);
            _timerText = GetComponent<TextMeshProUGUI>();
            _systemChannelSO.AddListener<GameStartEvent>(HandleGameStart);

            UpdateTimerText();
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat("Time", _elapsedTime);
            _systemChannelSO.RemoveListener<GameStartEvent>(HandleGameStart);
        }

        private void HandleGameStart(GameStartEvent evt)
        {
            StartTimer();
        }

        private void Update()
        {
            if (_timerRunning)
            {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= 3600f) // 1시간 경과 시 추가 처리
                {
                    _timerRunning = false;
                    Finish();
                }

                UpdateTimerText();
            }
        }

        private void Finish()
        {
            // 원하는 동작 추가 (예: 씬 이동, 게임 상태 변경 등)
            Debug.Log("Timer stopped after 1 hour.");
        }

        public void StartTimer()
        {
            _elapsedTime = PlayerPrefs.GetFloat("Time", 0);
            _timerRunning = true;
        }

        private void UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(_elapsedTime % 60f);

            _timerText.text = string.Format("경과 시간 : {0:00}:{1:00}", minutes, seconds);
        }
    }
}
