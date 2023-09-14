using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using static Define;

public class ItemData 
{
    [XmlAttribute]
    public int ID;
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public string description;
    [XmlAttribute]
    public AbilityType type;
    [XmlAttribute]
    public int value;
    [XmlAttribute]
    public int frequency;
    [XmlAttribute]
    public int price;
    [XmlIgnore]
    public ItemAbility ability;
}
[Serializable, XmlRoot("ArrayOfStatData")]
public class ItemDataLoader : ILoader<int, ItemData>
{
    [XmlElement("ItemData")]
    public List<ItemData> _itemData = new List<ItemData>();

    public Dictionary<int, ItemData> MakeDic()
    {
        Dictionary<int, ItemData> dic = new Dictionary<int, ItemData>();

        foreach (ItemData data in _itemData)
            dic.Add(data.ID, data);

        return dic;
    }

    public bool Validate()
    {
        return true;
    }
}