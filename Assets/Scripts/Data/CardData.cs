using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using static Define;

public class CardData
{
    [XmlAttribute]
    public int ID;
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public string description;
    [XmlAttribute] 
    public CardType type;
    [XmlAttribute] 
    public CardRarity rarity;
    [XmlAttribute]
    public TargetType target;
    [XmlAttribute]
    public int damage;
    [XmlAttribute]
    public int shield;
    [XmlAttribute]
    public int mana;
    [XmlAttribute]
    public int drawCard;
    [XmlAttribute]
    public int increasePower;
    [XmlAttribute]
    public int increaseAgility;
    [XmlAttribute]
    public int weakness;    //약화(공격피해 25%)
    [XmlAttribute]
    public int vulnerable;  //취약(받는 피해 50% 추가)
    [XmlAttribute]
    public string condition;//카드 사용 조건
    [XmlAttribute]
    public int getMana;
    [XmlAttribute]
    public CardLifeState state = CardLifeState.None;
    [XmlAttribute]
    public int attackNum = 1;
    [XmlAttribute]
    public float attackDiff = 0;
    [XmlAttribute]
    public int poisoning;   //중독
    [XmlAttribute]
    public int disturbance;
    [XmlArray("actions")]
    [XmlArrayItem("action")]
    public List<string> actionNames;

    [XmlIgnore]
    public List<ActionBase> actions = new();

    [XmlIgnore]
    public ICardCondition cardCondition;
    public void Upgrade() {
        damage += (int)attackDiff;
    }
}
[Serializable, XmlRoot("ArrayOfStatData")]
public class CardDataLoader : ILoader<int, CardData>
{
    [XmlElement("CardData")]
    public List<CardData> _cardData = new List<CardData>();

    public Dictionary<int, CardData> MakeDic()
    {
        Dictionary<int, CardData> dic = new Dictionary<int, CardData>();

        foreach (CardData data in _cardData)
            dic.Add(data.ID, data);

        return dic;
    }

    public bool Validate()
    {
        return true;
    }
}