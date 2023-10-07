using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FriendAbility
{
    public void Use();
    public void Setting();
}

public class InfinitySword : FriendAbility
{
    PlayerController playerController;
    public void Use()
    {
        if (playerController == null)
            playerController = Managers.UI.FindPopup<UI_BattlePopup>()._playerController;
        playerController.GetShield(6);
        playerController.GetSwordGauge(1);
    }
    public void Setting()
    {
        GameEvents.TurnEnd -= Use;
        GameEvents.TurnEnd += Use;
    }
}
