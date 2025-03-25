using UnityEngine;
using BIS.Manager;
namespace BIS.UI
{
    public class PopupUI : UIBase
    {
        public override bool Init()
        {
            if (base.Init() == false)
                return false;


            Managers.UI.SetCanvas(gameObject,false);
            return true;
        }
    }
}
