using UnityEngine;

namespace BIS.Define
{
    public static class Define
    {
        public const float UIDuration = 0.5f;
        public enum EUIEventType
        {
            DOWN,
            MOVE,
            ENTER,
            EXIT,
            CLICK,
            FLOAT
        }

        public static class MLayerMask
        {
            public static readonly LayerMask WhatIsGround = LayerMask.GetMask("Ground");
            public static readonly LayerMask WhatIsPlayer = LayerMask.GetMask("Player");
            public static readonly LayerMask WhatIsEnemy = LayerMask.GetMask("Enemy");
            public static readonly LayerMask WhatIsInteraction = LayerMask.GetMask("Interaction");
            public static readonly LayerMask WhatIsWall = LayerMask.GetMask("Wall");

        }

    }
    public enum EMainTabbar
    {
        MISSION,
        SKILL,
        APPEARANCE,
        PLAYGROUND,
        PROGRESSION
    }
    public enum EModelType
    {
        Int,
        Float,
        Bool
    }

    public enum EUIEvent
    {
        Click,
        PointerDown,
        PointerUp,
        Drag,
        PointerEnter,
        PointerExit
    }
    public enum EScene
    {
        Unknown,
        TitleScene,
        GameScene
    }

    public enum ERarity
    {

    }
    public enum EObjectTag
    {
        Player,
        Enemy
    }



    public enum EWeaponType
    {
        Rifle = 0,
        Shotgun = 1,
        Rail
    }

    public enum EInputType
    {
        Player
    }

    public enum EEnemyType
    {
        Bat =0,
        Eye,
        Fly,
        Ghost,
        Skeleton
    }
}

