using System.Collections.Generic;
using UnityEngine;

namespace BIS.Pool
{
    [CreateAssetMenu(menuName = "BIS/SO/PoollingList")]
    public class PoolListSO : ScriptableObject
    {
        [SerializeField] private List<PoolObj> _poollingList; public List<PoolObj> PolllingList { get { return _poollingList; } }
    }
}
