using EasyTransition;
using Map;
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
    MapManager mapManager;
    bool isGameData = true;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        mapManager = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<MapManager>();

        GetButton((int)Buttons.NewGameButton).gameObject.BindEvent(OnClickStartButton);
        GetButton((int)Buttons.LoadGameButton).gameObject.BindEvent(OnClickContinueButton);
        GetButton((int)Buttons.DictionaryButton).gameObject.BindEvent(OnClickDictionaryButton);
        GetButton((int)Buttons.SettingButton).gameObject.BindEvent(OnClickSettingButton);
        GetButton((int)Buttons.ExitGameButton).gameObject.BindEvent(OnClickExitButton);
        if (!Managers.Game.LoadGame()) {
            GetButton((int)Buttons.NewGameButton).transform.position = GetButton((int)Buttons.LoadGameButton).transform.position;
            Destroy(GetButton((int)Buttons.LoadGameButton).gameObject);
            isGameData = false;
        }
        StartCoroutine(ShaderShineGo(GetObject((int)GameObjects.GameTitleLogoImage)));

        return true;
    }

    void OnClickStartButton() {
        
        //이전에 플레이 게임 데이터가 있다면 팝업을 띄우고 결정하게 한다.
        if (isGameData) {
            Managers.UI.ShowPopupUI<UI_ConfirmPopup>().SetInfo(() =>
            {
                Managers.Game.Init();

                //초반 능력치 설정하는 팝업출력/ 이전 팝업 삭제
                Managers.UI.ShowPopupUI<UI_SelectChampAndItemPopup>();
                mapManager.ResetSavedMap();
            }, Managers.GetText(Define.DataResetConfirm));
        }
        else
        {
            Managers.Game.Init();
            mapManager.ResetSavedMap();

            Managers.UI.ShowPopupUI<UI_SelectChampAndItemPopup>();
        }
    }
    void OnClickContinueButton() { 

        if (isGameData)
        {
            TransitionManager.Instance().Transition(Managers.Resource.Load<TransitionSettings>("Transitions/Brush/Brush"), 0,
                    () => {
                        Managers.UI.ClosePopupUI(this);
                        Managers.UI.ShowPopupUI<UI_MapPopup>().SetInfo();
                    });
        }
    }
    void OnClickDictionaryButton() { 

    }
    void OnClickSettingButton() { 

    }
    void OnClickExitButton() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
