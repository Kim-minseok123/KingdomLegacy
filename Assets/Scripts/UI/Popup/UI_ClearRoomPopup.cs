using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ClearRoomPopup : UI_Popup
{
    enum Texts { 
        GetGoldText,
    }
    enum Buttons { 
        GetGoldButton,
        GetItemButton,
        GetCardButton,
        EndButton
    }
    public override bool Init()
    {
        if (!base.Init())
            return false;
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        if(Managers.Game.CurMapNode.Node.nodeType == Map.NodeType.MinorEnemy)
        {
            Destroy(GetButton((int)Buttons.GetItemButton).gameObject);
        }
        else
            GetButton((int)Buttons.GetItemButton).gameObject.BindEvent(GetItem);
        if (Managers.Game.isGoldPlusItem)
        {
            GetText((int)Texts.GetGoldText).text = "32 °ñµå È¹µæ";
        }
        GetButton((int)Buttons.GetCardButton).gameObject.BindEvent(GetCard);
        GetButton((int)Buttons.GetGoldButton).gameObject.BindEvent(GetGold);
        GetButton((int)Buttons.EndButton).gameObject.BindEvent(EndRoom);

        return true;
    }
    public void GetCard() { 
        Destroy(GetButton((int)Buttons.GetCardButton).gameObject);
        Managers.UI.ShowPopupUI<UI_ChooseClearCardPopup>();
    }
    public void GetItem() {
        Destroy(GetButton((int)Buttons.GetItemButton).gameObject);
        Managers.UI.ShowPopupUI<UI_ChooseClearItemPopup>();
    }
    public void GetGold() {
        if (Managers.Game.isGoldPlusItem) {
            Managers.Game.Money += 32;
        }
        else
            Managers.Game.Money += 25;
        Destroy(GetButton((int)Buttons.GetGoldButton).gameObject);
    }
    public void EndRoom() {

        if (Managers.Game.CurMapNode.Node.nodeType == Map.NodeType.Boss)
        {
            TransitionManager.Instance().Transition(Managers.Resource.Load<TransitionSettings>("Transitions/LinearWipe/LinearWipe"), 0,
                () => {
                    Managers.Game.Stage++;
                    Managers.Game.CurHp += (int)(Managers.Game.MaxHp * 0.4f);
                    if (Managers.Game.CurHp > Managers.Game.MaxHp) Managers.Game.CurHp = Managers.Game.MaxHp;
                    Managers.UI.FindPopup<UI_MapPopup>().SideBarOn();
                    Managers.UI.FindPopup<UI_MapPopup>().ShowMap();
                    Managers.UI.FindPopup<UI_MapPopup>().ResetMap();
                    Managers.UI.ClosePopupUI(this);
                    Camera.main.orthographicSize = 7;
                    Managers.UI.ClosePopupUI(Managers.UI.FindPopup<UI_BattlePopup>());

                });
        }
        else {
            TransitionManager.Instance().Transition(Managers.Resource.Load<TransitionSettings>("Transitions/LinearWipe/LinearWipe"), 0,
                () => {
                    Managers.UI.ClosePopupUI(this);
                    Camera.main.orthographicSize = 7;
                    Managers.UI.ClosePopupUI(Managers.UI.FindPopup<UI_BattlePopup>());
                    Managers.UI.FindPopup<UI_MapPopup>().SideBarOn();
                    Managers.UI.FindPopup<UI_MapPopup>().ShowMap();
                });
        }
        Managers.Game.SaveGame();
    }
}
