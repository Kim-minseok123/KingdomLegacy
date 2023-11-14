using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using EasyTransition;

public class UI_SelectChampAndItemPopup : UI_Popup
{
    enum Texts { 
        StartItemText,
        //...
        StartCharacterText,
        PlayerContentsText,
        PlayerMaxHpText,
        PlayerStartManaText,
        PlayerStartMoneyText,
        NextSelectText,
        PrevSelectText,
        StartGameText,
        ItemText
    }
    enum Buttons { 
        Item1,
        Item2,
        Item3,
        NextSelectButton,
        PrevSelectButton,
        StartGameButton,
    }
    enum GameObjects { 
        Bg1,
        Texts,
        SwordPlayer,
        ArcherPlayer,
        WizardPlayer,
    }
    public override bool Init() {
        if (base.Init() == false)
            return false;
        
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        Image image = GetObject((int)GameObjects.Bg1).GetComponent<Image>();
        image.material.SetFloat("_FadeAmount", 1f);
        
        StartCoroutine(FadeImage(image));

        Champion = new GameObject[3];
        Champion[0] = GetObject((int)GameObjects.ArcherPlayer);
        Champion[1] = GetObject((int)GameObjects.SwordPlayer);
        Champion[2] = GetObject((int)GameObjects.WizardPlayer);
        Color color = Champion[0].GetComponent<Image>().color;
        color.a = 0.5f;
        Champion[0].GetComponent<Image>().color = color;
        color = Champion[2].GetComponent<Image>().color;
        color.a = 0.5f;
        Champion[2].GetComponent<Image>().color = color;
        //바인딩
        GetButton((int)Buttons.NextSelectButton).gameObject.BindEvent(OnClickNextChampButton);
        GetButton((int)Buttons.PrevSelectButton).gameObject.BindEvent(OnClickPrevChampButton);
        GetButton((int)Buttons.StartGameButton).gameObject.BindEvent(OnClickStartGame);
        GetButton((int)Buttons.Item1).gameObject.BindEvent(OnClickItem1);
        GetButton((int)Buttons.Item2).gameObject.BindEvent(OnClickItem2);
        GetButton((int)Buttons.Item3).gameObject.BindEvent(OnClickItem3);

        return true;
    }

    IEnumerator FadeImage(Image image) {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/소멸", Managers.Game.EffectSound);

        float temp = 1f;
        while (temp > -0.1f) { 
            temp -= Time.deltaTime;
            image.material.SetFloat("_FadeAmount", temp);
            yield return null;
        }
        //타이틀 팝업 내리기
        Managers.Resource.Destroy(FindObjectOfType<UI_TitlePopup>().gameObject);

        GetText((int)Texts.StartItemText).text = "시작 유물 선택";
        GetText((int)Texts.StartCharacterText).text = "시작 영웅 선택";
        GetText((int)Texts.PlayerContentsText).text = Managers.GetText(Define.SwordPlayerContents);
        GetText((int)Texts.PlayerMaxHpText).text = Managers.GetText(Define.SwordPlayerMaxHp);
        GetText((int)Texts.PlayerStartManaText).text = Managers.GetText(Define.SwordPlayerStartMana);
        GetText((int)Texts.PlayerStartMoneyText).text = Managers.GetText(Define.SwordPlayerStartMoney);
        ChampionNumber = Define.SwordPlayerContents;
        GetText((int)Texts.NextSelectText).text = ">";
        GetText((int)Texts.PrevSelectText).text = "<";
        GetText((int)Texts.StartGameText).text = "시작하기";
    }
    GameObject[] Champion;
    int ChampionNumber = 0;
    bool isClickChampButton = false;
    void OnClickNextChampButton() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        if (isClickChampButton)
            return;
        isClickChampButton = true;

        GameObject temp = Champion[0];

        for (int i = 1; i < 3; i++) {
            Champion[i].transform.DOMove(Champion[i - 1].transform.position, 0.5f);
            Champion[i - 1] = Champion[i];
        }
        temp.transform.DOMove(Champion[^1].transform.position, 0.5f).OnComplete(ShowChamp); 
        Champion[^1] = temp;
    }
    void OnClickPrevChampButton() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        if (isClickChampButton)
            return;
        isClickChampButton = true;

        GameObject temp = Champion[^1];

        for (int i = 1; i > -1; i--)
        {
            Champion[i].transform.DOMove(Champion[i + 1].transform.position, 0.5f);
            Champion[i + 1] = Champion[i];
        }
        temp.transform.DOMove(Champion[0].transform.position, 0.5f).OnComplete(ShowChamp); 
        Champion[0] = temp;

    }
    void ShowChamp() {
        Debug.Log("챔피언 보여줌");
        for (int i = 0; i < 3; i++) { 
            Color color = Champion[i].GetComponent<Image>().color;
            color.a = 0.5f;
            Champion[i].GetComponent<Image>().color = color;
        }
        Color color2 = Champion[1].GetComponent<Image>().color;
        color2.a = 1f;
        Champion[1].GetComponent<Image>().color = color2;
        ShowChampText(Champion[1]);
        isClickChampButton = false;
    }

    void ShowChampText(GameObject gameObject)
    {

        if (gameObject == GetObject((int)GameObjects.ArcherPlayer))
        {
            GetText((int)Texts.PlayerContentsText).text = Managers.GetText(Define.ArcherPlayerContents);
            GetText((int)Texts.PlayerMaxHpText).text = Managers.GetText(Define.ArcherPlayerMaxHp);
            GetText((int)Texts.PlayerStartManaText).text = Managers.GetText(Define.ArcherPlayerStartMana);
            GetText((int)Texts.PlayerStartMoneyText).text = Managers.GetText(Define.ArcherPlayerStartMoney);
            ChampionNumber = Define.ArcherPlayerContents;
        }
        else if (gameObject == GetObject((int)GameObjects.SwordPlayer)) {
            GetText((int)Texts.PlayerContentsText).text = Managers.GetText(Define.SwordPlayerContents);
            GetText((int)Texts.PlayerMaxHpText).text = Managers.GetText(Define.SwordPlayerMaxHp);
            GetText((int)Texts.PlayerStartManaText).text = Managers.GetText(Define.SwordPlayerStartMana);
            GetText((int)Texts.PlayerStartMoneyText).text = Managers.GetText(Define.SwordPlayerStartMoney);
            ChampionNumber = Define.SwordPlayerContents;
        }
        else if (gameObject == GetObject((int)GameObjects.WizardPlayer)) {
            GetText((int)Texts.PlayerContentsText).text = Managers.GetText(Define.WizardPlayerContents);
            GetText((int)Texts.PlayerMaxHpText).text = Managers.GetText(Define.WizardPlayerMaxHp);
            GetText((int)Texts.PlayerStartManaText).text = Managers.GetText(Define.WizardPlayerStartMana);
            GetText((int)Texts.PlayerStartMoneyText).text = Managers.GetText(Define.WizardPlayerStartMoney);
            ChampionNumber = Define.WizardPlayerContents;
        }
    }
    int selectItem = 0;
    void OnClickStartGame()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        if (selectItem == 0 || ChampionNumber == Define.WizardPlayerContents || ChampionNumber == Define.ArcherPlayerContents) {
            return;
        }
        Managers.Sound.Play(Define.Sound.Effect, "Effect/넘기기", Managers.Game.EffectSound);

        TransitionManager.Instance().Transition(Managers.Resource.Load<TransitionSettings>("Transitions/Brush/Brush"), 0,
                    () => {
                        Managers.UI.ClosePopupUI();
                        Managers.Game.PlayerName = Champion[1].name;
                        Managers.Game.Stage = 1;
                        Managers.Game.Items.Add(selectItem);
                        Managers.UI.ShowPopupUI<UI_MapPopup>().SetInfo();
                        Managers.Sound.Play(Define.Sound.Effect, "Effect/넘기기", Managers.Game.EffectSound);
                    });
    }
    void OnClickItem1() {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        selectItem = 1;
        ItemData itemdata = Managers.Data.Items[1];
        GetText((int)Texts.ItemText).text = "<color=yellow>" + itemdata.name + "</color>\n\n" + itemdata.description;
    }
    void OnClickItem2()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        selectItem = 2;
        ItemData itemdata = Managers.Data.Items[2];
        GetText((int)Texts.ItemText).text = "<color=yellow>" + itemdata.name + "</color>\n\n" + itemdata.description;
    }
    void OnClickItem3()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effect/Click", Managers.Game.EffectSound);

        selectItem = 3;
        ItemData itemdata = Managers.Data.Items[3];
        GetText((int)Texts.ItemText).text = "<color=yellow>" + itemdata.name + "</color>\n\n" + itemdata.description;
    }
}
