using BIS.Core;
using DG.Tweening;
using System;
using UnityEngine;

namespace BIS.UI
{
    public class TipKeyUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _interactionChannelSO;
        private CanvasGroup _cG;
        private void Awake()
        {
            _cG = GetComponent<CanvasGroup>();
            _interactionChannelSO.AddListener<InteractionEnter>(HandleInteractionEvent);
        }
        private void OnDisable()
        {
            _interactionChannelSO.RemoveListener<InteractionEnter>(HandleInteractionEvent);
        }

        private void HandleInteractionEvent(InteractionEnter evt)
        {
            Debug.Log(evt.isEnter);
            if (evt.isEnter == true)
                _cG.DOFade(1, 0.5f);
            else
                _cG.DOFade(0, 0.5f);

        }
    }
}
