using System;

public static class GameEvents
{
    public static event Action BattleStart;
    public static event Action BattleEnd;
    public static event Action TurnStart;
    public static event Action<int> TurnValue;
    public static event Action TurnEnd;
    public static event Action GetItem;
    public static event Action TakeRest;
    public static event Action KillEnemy;
    public static event Action<CardData> UseCard;
    public static event Action GetCard;
    public static event Func<CardData, CardData> GetAttackCard;
    public static event Func<CardData, CardData> GetSkillCard;
    public static event Func<CardData, CardData> GetFriendCard;
    public static event Action LostHp;
    public static event Action ShuffleDeck;
    public static event Action ExitCard;
    public static event Action DrawCard;
    public static event Action<int> PreservationCards;
    public static void OnBattleStart()
    {
        BattleStart?.Invoke();
    }
    public static void OnBattleEnd()
    {
        BattleEnd?.Invoke();
    }
    public static void OnTurnStart()
    {
        TurnStart?.Invoke();
    }
    public static void OnTurnValue(int value)
    {
        TurnValue?.Invoke(value);
    }
    public static void OnTurnEnd()
    {
        TurnEnd?.Invoke();
    }
    public static void OnGetItem()
    {
        GetItem?.Invoke();
    }
    public static void OnTakeRest()
    {
        TakeRest?.Invoke();
    }
    public static void OnKillEnemy()
    {
        KillEnemy?.Invoke();
    }
    public static void OnUseCard(CardData card)
    {
        UseCard?.Invoke(card);
    }
    public static CardData OnGetAttackCard(CardData card)
    {
        return GetAttackCard?.Invoke(card);
    }
    public static CardData OnGetSkillCard(CardData card)
    {
        return GetSkillCard?.Invoke(card);
    }
    public static CardData OnGetFriendCard(CardData card)
    {
        return GetFriendCard?.Invoke(card);
    }
    public static void OnGetCard()
    {
        GetCard?.Invoke();
    }
    public static void OnLostHp() { 
        LostHp?.Invoke();
    }
    public static void OnShuffleDeck() {
        ShuffleDeck?.Invoke();
    }
    public static void OnExitCard() { 
        ExitCard?.Invoke();
    }
    public static void OnDrawCard() { 
        DrawCard?.Invoke();
    }
    public static void OnPreservationCards(int num) { 
        PreservationCards?.Invoke(num);
    }
}
