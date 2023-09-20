using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class ActionBase
{
    public abstract void StartAction(PlayerController player, CardData card, EnemyController enemy = null);
}
public static class ActionFactory
{
    public static ActionBase CreateAction(string actionName)
    {
        switch (actionName)
        {
            case "AttackAction":
                return new AttackAction();
            case "DefenseAction":
                return new DefenseAction();
            case "VulnerableAction":
                return new VulenrableAction();
            case "DrawAction":
                return new DrawAction();
            case "GetManaAction":
                return new GetManaAction();
            case "PoisonAction":
                return new PoisonAction();
            case "WeaknessAction":
                return new WeaknessAction();
            case "StressAction":
                return new StressAction();
            case "DizzinessAction":
                return new DizzinessAction();
            case "IncreasePowerAction":
                return new IncreasePowerAction();
            case "IncreaseAgilityAction":
                return new IncreaseAgilityAction();
            case "FriendAction" :
                return new FriendAction();
            default:
                throw new ArgumentException($"Unknown action: {actionName}");
        }
    }
}