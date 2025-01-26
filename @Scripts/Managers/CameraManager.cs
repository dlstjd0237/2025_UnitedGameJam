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
            if (_isShaking) return; // �̹� ��鸮�� ������ ���ο� ��鸲�� �������� ����

            _mainCame = Managers.GameScene.MainCamera;
            _mainCame.StartCoroutine(ShakeCameraCoroutine(offSet, amplitudeGain, frequencyGain, duration));
        }

        private IEnumerator ShakeCameraCoroutine(Vector3 offSet, float amplitudeGain, float frequencyGain, float duration)
        {
            _isShaking = true; // ��鸲 ����

            var cam = _mainCame.GetComponent<CinemachineBasicMultiChannelPerlin>();

            // ���� ���� ���ο� ��鸲 ȿ���� �߰�
            _currentOffset += offSet;
            _currentAmplitude += amplitudeGain;
            _currentFrequency += frequencyGain;

            cam.PivotOffset = _currentOffset;
            cam.AmplitudeGain = _currentAmplitude;
            cam.FrequencyGain = _currentFrequency;

            yield return new WaitForSeconds(duration);

            // ȿ���� ������ �߰��� ���� ����
            _currentOffset -= offSet;
            _currentAmplitude -= amplitudeGain;
            _currentFrequency -= frequencyGain;

            cam.PivotOffset = _currentOffset;
            cam.AmplitudeGain = _currentAmplitude;
            cam.FrequencyGain = _currentFrequency;

            _isShaking = false; // ��鸲 ����
        }
    }
}
