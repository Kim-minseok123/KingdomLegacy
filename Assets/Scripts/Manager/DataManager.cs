using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public interface ILoader<Key, Item>
{
    Dictionary<Key, Item> MakeDic();
    bool Validate();
}

public class DataManager
{
    //public StartData Start { get; private set; }

	public Dictionary<int, TextData> Texts { get; private set; }
    public Dictionary<int, CardData> Cards { get; private set; }
    public Dictionary<int , ItemData> Items { get; private set; }

	//public List<CollectionData> StatCollections { get; private set; }

	public void Init()
    {
        //Start = LoadSingleXml<StartData>("StartData");

        Texts = LoadXml<TextDataLoader, int, TextData>("TextData").MakeDic();
        //다른 캐릭터 선택 시 다른 카드 덱 선택
        Cards = LoadXml<CardDataLoader, int , CardData>("SwordCardData").MakeDic();
        foreach (CardData card in Cards.Values)
        {
            foreach (var name in card.actionNames)
            {
                card.actions.Add(ActionFactory.CreateAction(name));
            }
            card.cardCondition = CardConditionFactory.CreateCondition(card.condition);
        }
        GameEvents.PreservationCards -= (v) => { Cards[23].Upgrade(); };
        GameEvents.PreservationCards += (v) => { Cards[23].Upgrade(); };
        GameEvents.PreservationCards -= (v) => { Cards[24].Upgrade(); };
        GameEvents.PreservationCards += (v) => { Cards[24].Upgrade(); };
        Items = LoadXml<ItemDataLoader, int, ItemData>("ItemData").MakeDic();
        foreach (ItemData item in Items.Values) {
            item.ability = ItemAbilityFactory.CreateAbility(item);
        }

        //Shops = LoadXml<ShopDataLoader, int, ShopData>("ShopData").MakeDic();

        //var collectionLoader = LoadXml<CollectionDataLoader, int, CollectionData>("CollectionData");
        //StatCollections = collectionLoader._collectionData.Where(c => c.type == CollectionType.Stat).ToList();

        //var dialogueLoader = LoadXml<DialogueEventDataLoader, int, DialogueEventData>("DialogueEventData");
        //InferiorEvents = dialogueLoader._dialogueEventData.Where(e => e.enemyType == 1).ToList(); 
    }

	private Item LoadSingleXml<Item>(string name)
	{
		XmlSerializer xs = new XmlSerializer(typeof(Item));
		TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
		using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
			return (Item)xs.Deserialize(stream);
	}

	private Loader LoadXml<Loader, Key, Item>(string name) where Loader : ILoader<Key, Item>, new()
    {
        XmlSerializer xs = new XmlSerializer(typeof(Loader));
        TextAsset textAsset = Resources.Load<TextAsset>("Data/" + name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
            return (Loader)xs.Deserialize(stream);
    }
}
