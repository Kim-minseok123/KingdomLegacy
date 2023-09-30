using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoneCardCondition : ICardCondition
{
    public bool isUsable()
    {
        return true;
    }
}
public class UsedCardThreeOver : ICardCondition
{
    UI_BattlePopup battlePopup;
    public bool isUsable()
    {
        //if 카드를 3장 이상 사용한 턴에 사용가능
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        if(battlePopup._curTurnUseCard >= 3)
            return true;
        return false;
    }
}
public class HaveCardOneOver : ICardCondition {
    UI_BattlePopup battlePopup;
    public bool isUsable()
    {
        //카드를 한장 이상 손에 들고 있을 경우 사용가능
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        if (battlePopup._handCardsUI.Count >= 2)
            return true;
        return false;
    }
}
public class IsSpawnInfiSword : ICardCondition { 
    public bool isUsable() { 
        //무한의 검이 소환 되어 있을 경우 사용가능
        return false;
    }
}
public class HaveHpThirtyOver : ICardCondition {
    public bool isUsable() {
        //체력이 30 이상이면 사용가능
        if(Managers.Game.CurHp >= 30)
            return true;
        return false;
    }
}

