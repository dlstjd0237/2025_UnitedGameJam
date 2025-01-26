using UnityEngine;
using Unity.Cinemachine;
using BIS.Players;
using BIS.Init;
using BIS.Manager;

namespace BIS.Core
{
    public class Scene : InitBase
    {
        [SerializeField] private Player _player;
        [SerializeField] private CinemachineCamera _cam;

        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            Managers.GameScene.SetPlayer(_player);
            Managers.GameScene.SetMainCamera(_cam);

            return true;
        }
        private void Start()
        {
            _player.gameObject.SetActive(true);
        }
    }
}
