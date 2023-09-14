using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TitlePopup : UI_Popup
{
    enum Buttons { 
        NewGameButton,
        LoadGameButton,
        DictionaryButton,
        SettingButton,
        ExitGameButton,
    }
    enum Texts { 
        NewGameText,
        LoadGameText,
        DictionaryText,
        SettingText,
        ExitGameText,
        GameVersionText,
    }
    enum GameObjects { 
        GameTitleLogoImage,
    }

    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.NewGameButton).gameObject.BindEvent(OnClickStartButton);
        GetButton((int)Buttons.LoadGameButton).gameObject.BindEvent(OnClickContinueButton);
        GetButton((int)Buttons.DictionaryButton).gameObject.BindEvent(OnClickDictionaryButton);
        GetButton((int)Buttons.SettingButton).gameObject.BindEvent(OnClickSettingButton);
        GetButton((int)Buttons.LoadGameButton).gameObject.BindEvent(OnClickExitButton);

        StartCoroutine(ShaderShineGo(GetObject((int)GameObjects.GameTitleLogoImage)));

        return true;
    }

    void OnClickStartButton() {
        Debug.Log("새 게임 시작 버튼 클릭");
        
        //이전에 플레이 게임 데이터가 있다면 팝업을 띄우고 결정하게 한다.
        if (Managers.Game.LoadGame()) {
            Managers.UI.ShowPopupUI<UI_ConfirmPopup>().SetInfo(() =>
            {
                Managers.Game.Init();
                Managers.Game.SaveGame();

                //초반 능력치 설정하는 팝업출력/ 이전 팝업 삭제
                Managers.UI.ShowPopupUI<UI_SelectChampAndItemPopup>();
            }, Managers.GetText(Define.DataResetConfirm));
        }
        else
        {
            Managers.Game.Init();
            Managers.Game.SaveGame();

            Managers.UI.ShowPopupUI<UI_SelectChampAndItemPopup>();
        }
    }
    void OnClickContinueButton() { 
        Debug.Log("이어하기 버튼 클릭");

        if (Managers.Game.LoadGame())
        {

        }
        else {
            Debug.Log("저장된 게임이 없습니다.");
        }
    }
    void OnClickDictionaryButton() { 
        Debug.Log("백과사전 버튼 클릭");
    }
    void OnClickSettingButton() { 
        Debug.Log("세팅 버튼 클릭");
    }
    void OnClickExitButton() { 
        Debug.Log("종료 버튼 클릭");
    }

    IEnumerator ShaderShineGo(GameObject go)
    {
        float temp = 0;
        Image image = go.GetComponent<Image>();
        while (true)
        {
            temp += Time.deltaTime;

            if (temp > 1.0f)
            {
                temp = 0.0f;
                yield return new WaitForSeconds(2.0f);
            }

            image.material.SetFloat("_ShineLocation", temp);

            yield return null;
        }
    }
}
