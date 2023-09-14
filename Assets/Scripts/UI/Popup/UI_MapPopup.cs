using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MapPopup : UI_Popup
{
    enum Buttons {
        DeckButton,
        SettingButton,
    }
    enum Texts {
        HpText,
        MoneyText,
        ClearTimeText,
    }
    enum GameObjects { 
        MapObjects,
        MapManager,
        MapView,
        MapPlayerTracker,
    }
    enum Images {
        BackGroundImage,
    }
    public override bool Init()
    {
        Camera.main.orthographicSize = 7;
        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetObject((int)GameObjects.MapManager).GetComponent<MapManager>().StartGenerate();

        RefreshUI();

        return true;
    }
    void RefreshUI()
    {
        
    }
    public void SetInfo() { 
    
    }
}
