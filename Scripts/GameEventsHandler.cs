using System.Collections.Generic;
public class GameEventsHandler
{
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GamePauseEvent GamePauseEvent = new GamePauseEvent();
    public static readonly CoinPickUpEvent CoinPickUpEvent = new CoinPickUpEvent();
    public static readonly CoinPickUpAnimationEndEvent CoinPickUpAnimationEndEvent = new CoinPickUpAnimationEndEvent();
    public static readonly CardPickUpEvent CardPickUpEvent = new CardPickUpEvent();
    public static readonly CardNumberChangeEvent CardNumberChangeEvent = new CardNumberChangeEvent();
    public static readonly LevelChangeEvent LevelChangeEvent = new LevelChangeEvent();
    public static readonly GateInteractedEvent GateInteractedEvent = new GateInteractedEvent();
}
public class GameEvent { }

public class GamePauseEvent : GameEvent
{
    public bool SetPause;
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class GameStartEvent : GameEvent
{
    public int LevelSetLength;
    public float SetSpeedZ;
    public float SetSpeedX;
    public float SetSpeedZMax;
}

public class CoinPickUpEvent : GameEvent { }
public class CardPickUpEvent : GameEvent { }
public class CoinPickUpAnimationEndEvent : GameEvent { }

public class CardNumberChangeEvent : GameEvent
{
    public int Number;
    public int TotalNumber;
}
public class GateInteractedEvent : GameEvent
{
    public CardClass AffectedCard;
    public bool IsPickUp;
    public bool IsDouble;
    public List<CardClass> MoreCards;
}
public class LevelChangeEvent : GameEvent
{
    public int NumberOfLevel;
}

