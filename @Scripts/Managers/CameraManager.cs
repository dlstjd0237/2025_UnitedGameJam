using BIS.Manager;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace BIS.Manager
{
    public class CameraManager
    {
        private CinemachineCamera _mainCame;
        private Vector3 _currentOffset = Vector3.zero;
        private float _currentAmplitude = 0f;
        private float _currentFrequency = 0f;
        private bool _isShaking = false;

        public void ShakeCamera(Vector3 offSet, float amplitudeGain, float frequencyGain, float duration)
        {
            if (_isShaking) return; // 이미 흔들리고 있으면 새로운 흔들림을 시작하지 않음

            _mainCame = Managers.GameScene.MainCamera;
            _mainCame.StartCoroutine(ShakeCameraCoroutine(offSet, amplitudeGain, frequencyGain, duration));
        }

        private IEnumerator ShakeCameraCoroutine(Vector3 offSet, float amplitudeGain, float frequencyGain, float duration)
        {
            _isShaking = true; // 흔들림 시작

            var cam = _mainCame.GetComponent<CinemachineBasicMultiChannelPerlin>();

            // 기존 값에 새로운 흔들림 효과를 추가
            _currentOffset += offSet;
            _currentAmplitude += amplitudeGain;
            _currentFrequency += frequencyGain;

            cam.PivotOffset = _currentOffset;
            cam.AmplitudeGain = _currentAmplitude;
            cam.FrequencyGain = _currentFrequency;

            yield return new WaitForSeconds(duration);

            // 효과가 끝나면 추가된 값만 제거
            _currentOffset -= offSet;
            _currentAmplitude -= amplitudeGain;
            _currentFrequency -= frequencyGain;

            cam.PivotOffset = _currentOffset;
            cam.AmplitudeGain = _currentAmplitude;
            cam.FrequencyGain = _currentFrequency;

            _isShaking = false; // 흔들림 종료
        }
    }
}
