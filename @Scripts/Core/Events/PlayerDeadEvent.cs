using BIS.Core;
using UnityEngine;

namespace BIS.Core
{
    public static class PlayerDeadEvent
    {
        public static readonly DeadEvent EnemyDeadEvent = new DeadEvent();

    }

    public class DeadEvent : GameEvent
    {

    }
}
