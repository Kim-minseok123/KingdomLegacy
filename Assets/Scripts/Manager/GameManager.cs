using Map;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[SerializeField]
public class GameData
{
    //이 정보들만 있으면 게임을 언제든지 불러올 수 있다 하는 것 모두.
    public int stage;
    public int mana;
    public List<CardData> cards = new();
    public List<ItemData> items = new();
    public int startDrawCardNum;
    public bool isPreservation;
    public bool isDoublePoisoning;
    public int MaxHp;
    public int CurHp;
    public int Money;
    public string PlayerName;
    public int stageNumber;
    public int VulenrablePercent;
    public bool isManaDisappear;
    public MapNode curMapNode;
    public bool isGoldPlusItem;
    public bool isRest;
    public bool isEnhance;
    public bool isIntension;
    public bool isDiscount;
    public bool isResurrection;
    public bool isChoice;
    public bool isNonDebuff;
    public bool isDoubleItem;

}
public class GameManager : MonoBehaviour
{
    GameData _gameData = new GameData();
    public GameData SaveData {  get { return _gameData; } set { _gameData = value; } }
    public int Stage { get { return _gameData.stage; } set { _gameData.stage = value; } }
    public int Mana { get { return _gameData.mana; } set { _gameData.mana = value; } }
    public bool isPreservation { get { return _gameData.isPreservation; } set {_gameData.isPreservation = value; } }
    public bool isDoublePoisoning { get { return _gameData.isDoublePoisoning; } set { _gameData.isDoublePoisoning = value; } }
    public int MaxHp { get { return _gameData.MaxHp; } set { _gameData.MaxHp = value; } }
    public int CurHp { get { return _gameData.CurHp; } set { _gameData.CurHp = value; } }
    public int Money { get { return _gameData.Money; } set { _gameData.Money = value; } }
    public string PlayerName { get { return _gameData.PlayerName; } set { _gameData.PlayerName = value; } }
    public int StageNumber { get { return _gameData.stageNumber; } set { _gameData.stageNumber = value; } }
    public int VulenrablePercent { get { return _gameData.VulenrablePercent; } set { _gameData.VulenrablePercent = value; } }
    public bool isManaDisappear { get { return _gameData.isManaDisappear; } set { _gameData.isManaDisappear = value; } }
    public MapNode CurMapNode { get { return _gameData.curMapNode; } set { _gameData.curMapNode = value; } }
    public List<CardData> Cards { get {  return _gameData.cards; } set { _gameData.cards.AddRange(value);} }
    public List<ItemData> Items { get {  return _gameData.items; } set { _gameData.items.AddRange(value);} }
    public bool isGoldPlusItem { get { return _gameData.isGoldPlusItem; } set { _gameData.isGoldPlusItem = value; } }
    public bool isRest { get { return _gameData.isRest; } set { _gameData.isRest = value; } }
    public bool isIntension { get { return _gameData.isIntension; } set { _gameData.isIntension = value; } }
    public bool isEnhance { get { return _gameData.isEnhance; } set { _gameData.isEnhance = value; } }
    public bool isDiscount { get { return _gameData.isDiscount; } set { _gameData.isDiscount = value; } }
    public bool isResurrection { get { return _gameData.isResurrection; } set { _gameData.isResurrection = value; } }
    public bool isChoice { get { return _gameData.isChoice; } set { _gameData.isChoice = value; } }
    public bool isNonDebuff { get { return _gameData.isNonDebuff; } set { _gameData.isNonDebuff = value; } }
    public bool isDoubleItem { get { return _gameData.isDoubleItem; } set { _gameData.isDoubleItem = value; } }
    public int StartDrawCardNum { get {  return _gameData.startDrawCardNum;} set { _gameData.startDrawCardNum = value; } }
    public void Init() { 
        Stage = 1;
        StageNumber = 0;
        Mana = 3;
        for (int i = 0; i < 5; i++)
        {
            Cards.Add(Managers.Data.Cards[1]);
        }
        Cards.Add(Managers.Data.Cards[3]);
        for (int i = 0; i < 4; i++) {
            Cards.Add(Managers.Data.Cards[49]);
        }
        StartDrawCardNum = 5;
        isPreservation = false;
        isDoublePoisoning = false;
        MaxHp = 80;
        CurHp = MaxHp;
        Money = 99;
        VulenrablePercent = 50;
        PlayerName = "";
        isManaDisappear = false;
        CurMapNode = null;
        isEnhance = true;
        isRest = true;
        isIntension = true;
        isDiscount = false;
        isResurrection = false;
        isChoice = false;
        isNonDebuff = false;
        isDoubleItem = false;
    }
    
    #region Save & Load	
    public string _path = Application.persistentDataPath + "/SaveData.json";

    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        File.WriteAllText(_path, jsonStr);
        var Map = GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<MapManager>();
        Map.SaveMap();
        Debug.Log($"Save Game Completed : {_path}");
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);
        if (data != null)
        {
            Managers.Game.SaveData = data;
        }

        Debug.Log($"Save Game Loaded : {_path}");
        return true;
    }
    #endregion
}
