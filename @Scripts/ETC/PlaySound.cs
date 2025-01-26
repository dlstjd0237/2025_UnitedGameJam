using UnityEngine;
using System.Collections;
using BIS.Pool;

namespace BIS.ETC
{
    [RequireComponent(typeof(PoolReturn))]
    public class PlaySound : MonoBehaviour
    {
        private AudioSource _audio;
        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }
        public void Play()
        {
            _audio.Play();
            StartCoroutine(De());
        }

        private IEnumerator De()
        {
            yield return new WaitForSeconds(3);
            gameObject.SetActive(false);
        }
    }
}
