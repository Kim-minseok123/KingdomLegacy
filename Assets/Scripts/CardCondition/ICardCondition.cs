using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardCondition
{
    public bool isUsable();
}

public static class CardConditionFactory {
    public static ICardCondition CreateCondition(string CardCondition) {
        switch (CardCondition) {
            //ī�� ���ǵ� �߰� 
            case "UsedCardThreeOver":
                return new UsedCardThreeOver();
            case "HaveCardOneOver":
                return new HaveCardOneOver();
            case "IsSpawnInfiSword":
                return new IsSpawnInfiSword();
            default:
                return new NoneCardCondition();
        }
    }
}
