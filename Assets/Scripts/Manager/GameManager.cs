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
}
public class GameManager 
{
    GameData _gameData = new GameData();
    public GameData SaveData {  get { return _gameData; } set { _gameData = value; } }
    public int Stage { get { return _gameData.stage; } set { _gameData.stage = value; } }
    public int Mana { get { return _gameData.mana; } set { _gameData.mana = value; } }
    public bool isPreservation { get { return _gameData.isPreservation; } set {_gameData.isPreservation = value; } }
    public bool isDoublePoisoning { get { return _gameData.isDoublePoisoning; } set { _gameData.isDoublePoisoning = value; } }

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
    }
    
    #region Save & Load	
    public string _path = Application.persistentDataPath + "/SaveData.json";

    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        File.WriteAllText(_path, jsonStr);
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
