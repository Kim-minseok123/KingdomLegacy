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
    public int startDrawCardNum;
    public bool isPreservation;
    public bool isDoublePoisoning;
    public int MaxHp;
    public int CurHp;
    public int Money;
    public string PlayerName;
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


    public List<CardData> Cards { get {  return _gameData.cards; } set { _gameData.cards.AddRange(value);} }
    public int StartDrawCardNum { get {  return _gameData.startDrawCardNum;} set { _gameData.startDrawCardNum = value; } }
    public void Init() { 
        Stage = 1;
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
        PlayerName = "";
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
