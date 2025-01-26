using UnityEngine;
using BIS.Init;
using BIS.Inputs;
using BIS.Players;
using System;
using GMS;
using static BIS.Define.Define;
using BIS.Core;

namespace BIS.Entities
{
    public class EntityInteraction : MonoBehaviour, IEntityComponentInit
    {
        [SerializeField] private float _interactionRadius;
        [SerializeField] private Transform _interactionTrm;
        [SerializeField] private GameEventChannelSO _interactionEventSO;
        private Entity _entity;
        private PlayerInputSO _inputSO;
        public void Initalize(Entity entity)
        {
            _entity = entity;
            _inputSO = (_entity as Player).PlayerInput;
            _inputSO.InteractionEvent += HandleInteraction;
        }

        private void OnDisable()
        {
            _inputSO.InteractionEvent -= HandleInteraction;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Interaction"))
            {
                Debug.Log("dda");
                InteractionEnter evt = InteractionEvent.InteractionEnter;
                evt.isEnter = true;
                _interactionEventSO.RaiseEvent(evt);

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Interaction"))
            {
                Debug.Log("dda");
                InteractionEnter evt = InteractionEvent.InteractionEnter;
                evt.isEnter = false;
                _interactionEventSO.RaiseEvent(evt);

            }
        }

        private void HandleInteraction()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_interactionTrm.position, _interactionRadius, MLayerMask.WhatIsInteraction);
            foreach (var collider in colliders)
            {
                IInteraction interactable = collider.GetComponent<IInteraction>();
                if (interactable != null)
                {
                    interactable.Interaction();
                    break; // �ϳ��� ��ü�͸� ��ȣ�ۿ�
                }
            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            if (_interactionTrm != null)
                Gizmos.DrawWireSphere(_interactionTrm.position, _interactionRadius);
        }

    }
}
