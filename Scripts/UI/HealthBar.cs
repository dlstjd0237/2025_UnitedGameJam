using UnityEngine;
using DG.Tweening;

namespace BIS.UI
{
    public class HealthBar : UIBase
    {
        private enum Images
        {
            HealthFill,
            AttackFill
        }

        public override bool Init()
        {
            if (base.Init() == false)
                return false;


            BindImages(typeof(Images));



            return true;
        }

        public void SetValue(float current, float max)
        {
            GetImage((int)Images.HealthFill).DOFillAmount(current / max, 0.3f);
        }

        public void SetattackValue(float current, float max, float min)
        {
            GetImage((int)Images.AttackFill).DOFillAmount(current / max, 0.3f);
        }


    }
}
