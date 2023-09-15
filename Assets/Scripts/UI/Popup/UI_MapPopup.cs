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

    }
    enum Images {
        
    }
    int stage = 0;
    public override bool Init()
    {
        Camera.main.orthographicSize = 7;
        //BindObject(typeof(GameObjects));
        //BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        var BackGround = Managers.Resource.Instantiate("Map/BackGroundImage").GetComponent<SpriteRenderer>();
        BackGround.sprite = Managers.Resource.Load<Sprite>($"Sprites/BattleGround/BattleGround{stage}");

        var Map = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<MapManager>();
        Map.StartGenerate();

        RefreshUI();

        return true;
    }
    void RefreshUI()
    {
        
    }
    public void SetInfo() { 
        this.stage = Managers.Game.Stage;
    }
}
