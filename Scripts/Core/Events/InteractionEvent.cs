using UnityEngine;

namespace BIS.Core
{
    public static class InteractionEvent
    {
        public static readonly InteractionEnter InteractionEnter = new InteractionEnter();
    }

    public class InteractionEnter : GameEvent
    {
        public bool isEnter;
    }
}
