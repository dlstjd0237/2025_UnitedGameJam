using UnityEngine;

namespace BIS.Init
{
    public class InitBase : MonoBehaviour
    {
        protected bool _init = false;
        public virtual bool Init()
        {
            if (_init)
                return false;

            _init = true;

            return true;
        }

        private void Awake()
        {
            Init();
        }
    }
}

