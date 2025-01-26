using BIS.Define;
using BIS.Entities;
using BIS.Init;
using BIS.Pool;
using System.Collections;
using UnityEngine;

namespace BIS.Bullets
{
    [RequireComponent(typeof(PoolReturn))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Bullet : InitBase
    {
        protected Rigidbody2D _rigidbody2D;
        protected Collider2D _collider2D;
        protected SpriteRenderer _render;
        [SerializeField]
        protected EObjectTag _targetTag;
        private float _dmaamge;

        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _render = GetComponent<SpriteRenderer>();

            return true;
        }

        private void OnEnable()
        {
            _collider2D.enabled = true;
            _render.enabled = true;
        }

        public void SetMovement(Vector2 dir, float speed, float damage, float lifeTime)
        {
            StartCoroutine(ObejctActive(false, lifeTime));
            _rigidbody2D.linearVelocity = dir * speed;
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

            if (collision.CompareTag("Wall"))
            {
                collisionEvent(collision);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall"))
            {
                collisionEvent(collision);
            }
        }

        protected virtual void collisionEvent(Collider2D collision)
        {
            //Vector3 randomVector;

            PoolManager.SpawnFromPool("BulletParticle", transform.position);
            PoolManager.SpawnFromPool("BulletEnemyAttackVFX", transform.position);

            gameObject.SetActive(false);
        }
    }
}
