using BIS.Manager;
using UnityEngine;

namespace BIS.UI
{
    public class SceneUI : UIBase
    {
        public override bool Init()
        {
            if (base.Init() == false)
                return false;


            Managers.UI.SetCanvas(gameObject, false);
            return true;
        }
    }
}
