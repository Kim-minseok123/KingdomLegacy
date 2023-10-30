using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Extension;
public class UI_ChooseClearCardPopup : UI_Popup
{
    enum Transforms { 
        CardTransform1,
        CardTransform2,
        CardTransform3,
    }
    enum Buttons { 
        EndButton,
    }

    public override bool Init()
    {
        if (!base.Init()) { 
            return false;
        }

        BindButton(typeof(Buttons));
        Bind<Transform>(typeof(Transforms));

        if (Managers.Game.CurMapNode.Node.nodeType == Map.NodeType.Boss)
        {
            BossStage();
        }
        else {
            if (Managers.Game.Stage == 1)
                NonBoss1Stage();
            else
                NonBossStage();
        }

        GetButton((int)Buttons.EndButton).gameObject.BindEvent(EndSelect);
        return true;
    }
    public void EndSelect() {
        GetComponent<Animator>().SetTrigger("Off");
    }
    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
    public void NonBoss1Stage() {
        CardData Randcard;
        for (int i = 0; i < 3; i++) {
            do
            {
                Randcard = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
            } while ((Randcard.rarity != Define.Rarity.Normal && Randcard.rarity != Define.Rarity.Rare) || Randcard.ID >=119 || Randcard.ID == 1 || Randcard.ID == 2 || Randcard.ID == 49 || Randcard.ID == 50);
            var card = Managers.Resource.Instantiate("UI/SubItem/UI_ClearCard", transform);
            card.GetComponent<UI_NonBattleCard>().SetInfo(Randcard.ID);
            card.transform.position = Get<Transform>(i).position;
        }
    }
    public void NonBossStage()
    {
        CardData Randcard;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                Randcard = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
            } while ((Randcard.rarity != Define.Rarity.Normal && Randcard.rarity != Define.Rarity.Rare && Randcard.rarity != Define.Rarity.Unique) || Randcard.ID >= 119 || Randcard.ID == 1 || Randcard.ID == 2 || Randcard.ID == 49 || Randcard.ID == 50);
            var card = Managers.Resource.Instantiate("UI/SubItem/UI_ClearCard", transform);
            card.GetComponent<UI_NonBattleCard>().SetInfo(Randcard.ID);
            card.transform.position = Get<Transform>(i).position;
        }
    }
    public void BossStage() {
        CardData Randcard;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                Randcard = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
            } while ((Randcard.rarity != Define.Rarity.Legend)|| Randcard.ID >= 119);
            var card = Managers.Resource.Instantiate("UI/SubItem/UI_ClearCard", transform);
            card.GetComponent<UI_NonBattleCard>().SetInfo(Randcard.ID);
            card.transform.position = Get<Transform>(i).position;
        }
    }
}
