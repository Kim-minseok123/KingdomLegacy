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
        Debug.Log("�� ���� ���� ��ư Ŭ��");
        
        //������ �÷��� ���� �����Ͱ� �ִٸ� �˾��� ���� �����ϰ� �Ѵ�.
        if (Managers.Game.LoadGame()) {
            Managers.UI.ShowPopupUI<UI_ConfirmPopup>().SetInfo(() =>
            {
                Managers.Game.Init();
                Managers.Game.SaveGame();

                //�ʹ� �ɷ�ġ �����ϴ� �˾����/ ���� �˾� ����
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
        Debug.Log("�̾��ϱ� ��ư Ŭ��");

        if (Managers.Game.LoadGame())
        {

        }
        else {
            Debug.Log("����� ������ �����ϴ�.");
        }
    }
    void OnClickDictionaryButton() { 
        Debug.Log("������� ��ư Ŭ��");
    }
    void OnClickSettingButton() { 
        Debug.Log("���� ��ư Ŭ��");
    }
    void OnClickExitButton() { 
        Debug.Log("���� ��ư Ŭ��");
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
