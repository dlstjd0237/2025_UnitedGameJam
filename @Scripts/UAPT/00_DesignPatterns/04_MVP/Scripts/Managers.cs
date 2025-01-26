using BIS.Manager;
using UnityEngine;

namespace BIS.Manager
{
    public class Managers : MonoBehaviour
    {
        private static Managers s_inctance;
        private static Managers Instacne
        {
            get
            {
                Init();
                return s_inctance;
            }
        }

        private ResourceManager _resource = new ResourceManager();
        private UIManager _ui = new UIManager();
        private CameraManager _camera = new CameraManager();
        private GameSceneManager _gameScene = new GameSceneManager();
        private EnemyManager _enemy = new EnemyManager();
        //private Game

        public static ResourceManager Resource { get { return Instacne._resource; } }
        public static UIManager UI { get { return Instacne._ui; } }
        public static CameraManager Camera { get { return Instacne._camera; } }
        public static GameSceneManager GameScene { get { return Instacne._gameScene; } }
        public static EnemyManager Enemy { get { return Instacne._enemy; } }

        private static GameObject _go;
        public static Transform Root => _go.transform;

        private static void Init()
        {
            if (s_inctance == null)
            {
                _go = GameObject.Find("@Managers");
                if (_go == null)
                {
                    _go = new GameObject { name = "@Managers" };
                    _go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(_go);

                //√ ±‚»≠
                s_inctance = _go.GetComponent<Managers>();
            }
        }
    }

}
