namespace BIS.Core
{
    public static class SystemEvent
    {
        public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    }

    public class GameStartEvent : GameEvent
    {
        public int stageTime;
    }
}
