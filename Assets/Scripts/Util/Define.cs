using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Define
{
	public enum TargetType { 
		Player,
		Enemy,
		AllEnemy,
		Random,
	}
	public enum CardLifeState { 
		None,
        Extinction,
        Preservation,
        Volatility,
    }
	public enum CardType { 
		Attack,
		Skill,
		Friend
	}
	public enum CardRarity { 
		Normal,
		Rare,
        Unique,
		Legend
    }
	public enum AbilityType { 
		TurnStart,
        TurnValue,	//특정 턴
        TurnEnd,
		BattleEnd,
		GetItem,
		TakeRest,
		KillEnemy,
		UseCard,
		GetCard,
		LostHp,
        ShuffleDeck,
		BattleStart,
		ExitCard,
		DrawCard,
        PreservationCards,
        EnemyTurnStart,
        EnemyTurning,
        EnemyTurnEnd,
    }
    public enum UIEvent
	{
		Click,
		Pressed,
		PointerDown,
		PointerUp,
		PointerEnter,
		PointerExit,
		Drag,
		DragEnd,
	}
	public enum Intention { 
		Attack,
		AttackDebuff,
		AttackDefense,
		AttackMany,
		Defense,
		DefenseBuff,
		Buff,
		DeBuff,
		Nothing,
	}

    public enum Scene
	{
		Unknown,
		Game,
	}

	public enum Sound
	{
		Bgm,
		Effect,
		Speech,
		Max,
	}

	public enum AnimState
	{
		None,
		Idle,
		Sweat,
		Walking,
		Working,
		Attack,
	}
	public const int DataResetConfirm = 20022;
	public const int GoTitle = 20023;
	public const int SowrdPlayerContents = 20024;
    public const int SowrdPlayerMaxHp = 20025;
    public const int SowrdPlayerStartMana = 20026;
    public const int SowrdPlayerStartMoney = 20027;
    public const int ArcherPlayerContents = 20028;
    public const int ArcherPlayerMaxHp = 20029;
    public const int ArcherPlayerStartMana = 20030;
    public const int ArcherPlayerStartMoney = 20031;
    public const int WizardPlayerContents = 20032;
    public const int WizardPlayerMaxHp = 20033;
    public const int WizardPlayerStartMana = 20034;
    public const int WizardPlayerStartMoney = 20035;
    public const int Weakness = 20036;
    public const int Vulenerable = 20037;
    public const int Power = 20038;
    public const int Agility = 20039;
    public const int Poisoning = 20040;
    public const int Shield = 20041;
    public const int dePower = 20042;
    public const int infinitySword = 20043;
	public const int Barrier = 20044;
}
