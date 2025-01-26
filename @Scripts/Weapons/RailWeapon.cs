using BIS.Define;
using BIS.Entities;
using BIS.Init;
using BIS.Pool;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace BIS.Weapons
{
    [RequireComponent(typeof(PoolReturn))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class RailWeapon : InitBase
    {
        protected Rigidbody2D _rigidbody2D;
        protected Collider2D _collider2D;
        protected SpriteRenderer _render;
        [SerializeField]
        protected EObjectTag _targetTag;
        private float _dmaamge;

        public void SetMovement(Vector2 dir, float speed, float damage, float lifeTime)
        {
            transform.DOScaleX(5, lifeTime - 0.25f);
            transform.DOScaleY(100, 0.5f);
            StartCoroutine(ObejctActive(false, lifeTime));
            _dmaamge = damage;
        }

        private IEnumerator ObejctActive(bool value, float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(value);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_targetTag.ToString()) && collision.TryGetComponent<Entity>(out Entity entity))
            {
                entity.GetCompo<EntityHealth>().ApplyDamage(_dmaamge);
                _collider2D.enabled = false;
                _render.enabled = false;

                collisionEvent(collision);
            }
        }
        protected virtual void collisionEvent(Collider2D collision)
        {
            gameObject.SetActive(false);
        }
    }
}
