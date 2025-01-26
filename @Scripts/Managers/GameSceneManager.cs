using BIS.Players;
using System;
using Unity.Cinemachine;
using UnityEngine;
namespace BIS
{
    public class GameSceneManager
    {
        public Player Player { get; private set; }
        public CinemachineCamera MainCamera { get; private set; }


        public event Action<int> CoinValueChangeEvent;
        private int _coin = 0;
        public int Coin => _coin;

        //private

        public void AddCoint(int value)
        {
            _coin += value;
            CoinValueChangeEvent?.Invoke(_coin);
        }

        public void RemoveCoint(int value)
        {
            _coin -= Mathf.Max(value, 0);
            CoinValueChangeEvent?.Invoke(_coin);
        }

        public void GameReset()
        {
            _coin = 0;
            CoinValueChangeEvent?.Invoke(_coin);
        }




        public void SetPlayer(Player player)
        {
            Player = player;
        }

        public void SetMainCamera(CinemachineCamera cam)
        {
            MainCamera = cam;
        }
    }
}
