using UnityEngine;

namespace BIS.Core
{
    public class TimerEventHandler : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _systemChannelSO;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameStartEvent evt = SystemEvent.GameStartEvent;

                _systemChannelSO.RaiseEvent(evt);
                Destroy(gameObject);
            }
        }
    }
}
