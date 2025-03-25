using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Manager;
using BIS.Players;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyParisIdleState : EntityState
    {
        private Player _player;
        public EnemyParisIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _player = Managers.GameScene.Player;
        }

        public override void Update()
        {
            base.Update();
            Vector3 direction = (_player.transform.position - _entity.transform.position).normalized;
            _entity.transform.position += direction * _entity.EntityStat.moveSpeed.GetValue() * Time.deltaTime;
        }
    }
}
