using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace BIS.UI
{
    public class DeadUI : UIBase
    {
        [SerializeField] private string _retryScene;

        private enum Texts
        {
            RetryText,
            ExitText
        }
        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            BindTexts(typeof(Texts));


            BindEvent(GetText((int)Texts.RetryText).gameObject, HandleRetryTextClick, Define.EUIEvent.Click);
            BindEvent(GetText((int)Texts.ExitText).gameObject, HandleExitTextClick, Define.EUIEvent.Click);

            return true;
        }

        private void HandleExitTextClick(PointerEventData evt)
        {
            SceneControlManager.FadeOut(() => Application.Quit());
        }

        private void HandleRetryTextClick(PointerEventData evt)
        {
            SceneControlManager.FadeOut(() => SceneManager.LoadScene(_retryScene));
            PlayerPrefs.SetFloat("Time", 0);
        }
    }
}
