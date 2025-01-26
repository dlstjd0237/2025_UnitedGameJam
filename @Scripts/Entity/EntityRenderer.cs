using BIS.Animators;
using BIS.Init;
using System;
using UnityEngine;

namespace BIS.Entities
{
    public class EntityRenderer : AnimateRenderer, IEntityComponentInit
    {
        protected Entity _entity;
        public event Action<int> FacingDirectionChangeEvent;
        [field: SerializeField] public int FacingDirection { get; private set; } = 1;
        public void Initalize(Entity entity)
        {
            _entity = entity;
        }

        #region FlipController

        public void Flip()
        {
            FacingDirection *= -1;
            FacingDirectionChangeEvent?.Invoke(FacingDirection);
            //transform.parent.Rotate(0, 180f* FacingDirection, 0);
            transform.parent.localScale = new Vector3(FacingDirection, 1, 1);
        }

        public void FlipController(float normalizeXMove)
        {
            if (Mathf.Abs(FacingDirection + normalizeXMove) < 0.5f)
                Flip();
        }

        #endregion
    }
}
