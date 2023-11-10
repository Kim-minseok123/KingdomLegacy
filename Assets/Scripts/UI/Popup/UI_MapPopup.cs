using DG.Tweening;
using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MapPopup : UI_Popup
{
    enum Buttons {
        DeckButton,
        SettingButton,
        ShopButton,
    }
    enum Texts {
        HpText,
        MoneyText,
        ClearTimeText,
    }
    enum GameObjects { 
        ItemList,
        SideBar,
        Menu,
        Contents,
    }
    enum Images {

    }
    int stage = 0;
    float _time = 0f;
    string _timeText = "";
    List<int> _itemList = new();
    SpriteRenderer Background;
    MapManager MapManager;
    GameObject map;
    public override bool Init()
    {
        Camera.main.orthographicSize = 7;
        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        Background = Managers.Resource.Instantiate("Map/BackGroundImage").GetComponent<SpriteRenderer>();
        Background.sprite = Managers.Resource.Load<Sprite>($"Sprites/BattleGround/BattleGround{stage}");


        GetButton((int)Buttons.DeckButton).gameObject.BindEvent(ClickDeckButton);

        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(ShopOn);

        
        MapManager = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<MapManager>();
        MapManager.StartGenerate();
        _time = Managers.Game.ClearTime;
        InitItem();
        Managers.Sound.Play(Define.Sound.Bgm, $"Bgm/Stage{Managers.Game.Stage}", Managers.Game.BgmSound);

        RefreshUI();

        return true;
    }
    void RefreshUI()
    {
        GetText((int)Texts.HpText).text = Managers.Game.CurHp.ToString() + " / " + Managers.Game.MaxHp.ToString();
        GetText((int)Texts.MoneyText).text = Managers.Game.Money.ToString();
        GetText((int)Texts.ClearTimeText).text = _timeText;
    }
    public void ClickDeckButton() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        Managers.UI.ShowPopupUI<UI_ShowCardsListPopup>().SetInfo();
    }
    public void SideBarOn() {
        GetObject((int)GameObjects.SideBar).SetActive(true);
    }
    public void SideBarOff()
    {
        GetObject((int)GameObjects.SideBar).SetActive(false);
    }
    public void SetInfo() {
        
        stage = Managers.Game.Stage;
    }
    public void Update()
    {
        _time += Time.deltaTime;
        int hours = (int)(_time / 3600);
        int minutes = (int)((_time % 3600) / 60);
        int seconds = (int)(_time % 60);
        Managers.Game.ClearTime = _time;
        _timeText = string.Format("{0:D2} : {1:D2} : {2:D2}", hours, minutes, seconds);
        RefreshUI();
    }
    
    public void ShopOn() {
        SideBarOff();
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        Managers.UI.ShowPopupUI<UI_ShopPopup>();
    }
    
    public void InitItem() {
        _itemList = Managers.Game.Items;

        for (int i = 0; i < _itemList.Count; i++) {
            var item = Managers.Resource.Instantiate("UI/SubItem/UI_Item", GetObject((int)GameObjects.ItemList).transform).GetComponent<UI_Item>();
            item.SetInfo(_itemList[i], 1);
            item._itemData.ability.Setting();
        }
        int tempMoney = Managers.Game.Money;
        int tempMaxHp = Managers.Game.MaxHp;
        int tempCurHp = Managers.Game.CurHp;
        GameEvents.OnGetItem();
        Managers.Game.Money = tempMoney;
        Managers.Game.MaxHp = tempMaxHp;
        Managers.Game.CurHp = tempCurHp;
        Managers.Game.SaveGame();
    }
    public void AddItem(int id) {
        Managers.Resource.Instantiate("UI/SubItem/UI_Item", GetObject((int)GameObjects.ItemList).transform).GetComponent<UI_Item>().SetInfo(id, 1);
    }
    public void ResetMap()
    {
        Background.sprite = Managers.Resource.Load<Sprite>($"Sprites/BattleGround/BattleGround{Managers.Game.Stage}");
        MapManager.GenerateNewMap();
    }
    public void HideMap() {
        if(map == null)
            map = GameObject.Find("OuterMapParent");
        map.SetActive(false);
    }
    public void ShowMap() {
        Managers.Sound.Play(Define.Sound.Bgm, $"Bgm/Stage{Managers.Game.Stage}", Managers.Game.BgmSound);
        if (map == null)
            map = GameObject.Find("OuterMapParent");
        map.SetActive(true);
    }
}
