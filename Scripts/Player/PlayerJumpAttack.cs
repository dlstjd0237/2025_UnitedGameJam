using UnityEngine;
using BIS.Init;
using BIS.Entities;
using BIS.Enemys;
using BIS.Pool;

namespace BIS.Players
{
    public class PlayerJumpAttack : MonoBehaviour, IEntityComponentInit
    {
        private Player _player;
        public void Initalize(Entity entity)
        {
            _player = entity as Player;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (collision.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.GetCompo<EntityHealth>().ApplyDamage(10);
                }
                PoolManager.SpawnFromPool("BulletParticle", collision.transform.position);
                _player.ChangeState("JUMP");
            }
        }
    }
}
