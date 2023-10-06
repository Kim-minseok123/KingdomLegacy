using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectCardPopup : UI_Popup
{
    enum GameObjects { 
        CardContent,
    }
    enum Buttons { 
        SelectButton,
    }
    public List<UI_Card> cards = new();
    public List<GameObject> cardList = new();
    public int numofThrow = 0;
    public CardData useCard;
    public GameObject selectCard = null;
    PlayerController player;
    EnemyController enemy;
    public int cases;
    int Damage;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        for (int i = 0; i < cards.Count; i++) {
            //if (cards[i]._cardData.OnlyCardnum == useCard.OnlyCardnum) continue;
            var card = Managers.UI.MakeSubItem<UI_Card>(GetObject((int)GameObjects.CardContent).transform).SetInfo(cards[i]._cardData);
            //ÅøÆÁ Ãß°¡
            cardList.Add(card.gameObject);
            card.gameObject.BindEvent(() => { SelectCard(card.gameObject); });
        }
        GetButton((int)Buttons.SelectButton).gameObject.BindEvent(SelectCardButton);
        RefreshUI();
        return true;
    }
    public void RefreshUI() { 
        
    }
    public void SetInfo(List<UI_Card> cards, int numofThrow, int cases, CardData useCard = null, PlayerController player = null, EnemyController enemy = null, int Damage = 0) {
        this.cards = cards;
        this.numofThrow = numofThrow;
        this.cases = cases;
        this.useCard = useCard;
        this.player = player;
        this.enemy = enemy;
        this.Damage = Damage;
        cardList.Clear();
    }
    public void SelectCardButton() {
        if (selectCard == null) return;
        int i = cardList.IndexOf(selectCard);

        Managers.UI.FindPopup<UI_BattlePopup>().ThrowCard(i);
        if (cases == 0)
            player.AttackEnemy(Damage, enemy);
        else if (cases == 1)
            player.GetShield(Damage);
        Managers.UI.ClosePopupUI();
    }
    public void SelectCard(GameObject go) { 
        selectCard = go;
        for (int y = 0; y < cardList.Count; y++) {
            if (selectCard == cardList[y])
            {
                cardList[y].GetComponent<Image>().material.EnableKeyword("INNEROUTLINE_ON");
            }
            else {
                cardList[y].GetComponent<Image>().material.DisableKeyword("INNEROUTLINE_ON");

            }
        }
    }
}
