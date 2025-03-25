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
        private TextMeshProUGUI _timerText;  // �ؽ�Ʈ UI�� �Ҵ��� ����
        private float _elapsedTime = 0f;  // ��� �ð�
        private bool _timerRunning = false;  // Ÿ�̸� ����

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

                if (_elapsedTime >= 3600f) // 1�ð� ��� �� �߰� ó��
                {
                    _timerRunning = false;
                    Finish();
                }

                UpdateTimerText();
            }
        }

        private void Finish()
        {
            // ���ϴ� ���� �߰� (��: �� �̵�, ���� ���� ���� ��)
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

            _timerText.text = string.Format("��� �ð� : {0:00}:{1:00}", minutes, seconds);
        }
    }
}
