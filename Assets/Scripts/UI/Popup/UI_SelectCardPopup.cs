using DG.Tweening;
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
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        for (int i = 0; i < cards.Count; i++) {
            //if (cards[i]._cardData.OnlyCardnum == useCard.OnlyCardnum) continue;
            var card = Managers.UI.MakeSubItem<UI_Card>(GetObject((int)GameObjects.CardContent).transform).SetInfo(cards[i]._cardData);
            //ÅøÆÁ Ãß°¡
            cardList.Add(card.gameObject);
            card.gameObject.BindEvent(() => { SelectCard(card.gameObject); });
            card.gameObject.BindEvent((go) => { ScaleUp(card.gameObject); },Define.UIEvent.PointerEnter);
            card.gameObject.BindEvent(() => { ScaleDown(card.gameObject); }, Define.UIEvent.PointerExit);
        }

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

    void OnComplete()
    {
        Managers.UI.ClosePopupUI(this);
    }
    public void SelectCard(GameObject go) {
        selectCard = go;
        int i = cardList.IndexOf(selectCard);

        Managers.UI.FindPopup<UI_BattlePopup>().ThrowCard(i);
        if (cases == 0)
            player.AttackEnemy(Damage, enemy);
        else if (cases == 1)
            player.GetShield(Damage);
        GetComponent<Animator>().SetTrigger("Off");
    }
    public void ScaleUp(GameObject go){
        go.transform.DOScale(new Vector3(1.15f, 1.15f, 1), 0.2f).SetEase(Ease.OutSine);
    }
    public void ScaleDown(GameObject go) { 
        go.transform.DOScale(new Vector3(1f, 1f, 1), 0.2f).SetEase(Ease.OutSine);
    }
}
