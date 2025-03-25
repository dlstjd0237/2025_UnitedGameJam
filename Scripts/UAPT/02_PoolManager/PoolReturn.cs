using UnityEngine;

namespace BIS.Pool
{
    public class PoolReturn : MonoBehaviour
    {
        private void OnDisable()
        {
            PoolManager.ReturnToPool(gameObject);
        }
    }
}
