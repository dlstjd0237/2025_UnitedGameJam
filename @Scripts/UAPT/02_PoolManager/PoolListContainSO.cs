using System.Collections.Generic;
using UnityEngine;

namespace BIS.Pool
{
    [CreateAssetMenu(menuName = "BIS/SO/PoolListContain")]
    public class PoolListContainSO : ScriptableObject
    {
        [SerializeField] private List<PoolListSO> _poolListContainList; public List<PoolListSO> PoolListContainList { get { return _poolListContainList; } }
    }
}
