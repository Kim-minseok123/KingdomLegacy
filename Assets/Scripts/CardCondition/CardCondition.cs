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
        //if ī�带 3�� �̻� ����� �Ͽ� ��밡��
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
        //ī�带 ���� �̻� �տ� ��� ���� ��� ��밡��
        if (battlePopup == null)
            battlePopup = Managers.UI.FindPopup<UI_BattlePopup>();
        if (battlePopup._handCardsUI.Count >= 2)
            return true;
        return false;
    }
}
public class IsSpawnInfiSword : ICardCondition { 
    public bool isUsable() { 
        //������ ���� ��ȯ �Ǿ� ���� ��� ��밡��
        return false;
    }
}
public class HaveHpThirtyOver : ICardCondition {
    public bool isUsable() {
        //ü���� 30 �̻��̸� ��밡��
        if(Managers.Game.CurHp >= 30)
            return true;
        return false;
    }
}

