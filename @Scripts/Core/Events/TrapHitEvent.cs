using UnityEngine;

namespace BIS.Core
{
    public static class TrapHitEvent
    {
        public static readonly HitEvent PlayerTrapHitEvent = new();
        public static readonly FallEvent PlayerFallEvent = new();
    }

    public class HitEvent : GameEvent
    {
    }

    public class FallEvent : GameEvent
    {
        public bool isFall = false;
    }
}
