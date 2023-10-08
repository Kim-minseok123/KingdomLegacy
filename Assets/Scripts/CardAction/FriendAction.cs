using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 91: case 92:
                FriendAbility Hod = new Hod();
                player._battleScene.MakeFriend("호드", Hod, card);
                break;
            case 93: case 94:
                FriendAbility CalieCarly = new CalieCarly();
                player._battleScene.MakeFriend("캘리칼리", CalieCarly, card);
                break;
            case 95: case 96:
                FriendAbility Davidson = new Davidson();
                player._battleScene.MakeFriend("데이비슨", Davidson, card);
                break;
            case 97: case 98:
                FriendAbility Bedford = new Bedford();
                player._battleScene.MakeFriend("배드퍼드", Bedford, card);
                player.GetPower(card.anyAttribute);
                break;
            case 99: case 100:
                FriendAbility Grafton = new Grafton();
                player._battleScene.MakeFriend("그래프턴", Grafton, card);
                break;
            case 101: case 102:
                FriendAbility Beaufort = new Beaufort();
                player._battleScene.MakeFriend("보퍼트", Beaufort, card);
                break;
            case 103: case 104:
                FriendAbility Devon = new Devon();
                player._battleScene.MakeFriend("데본", Devon, card);
                break;
            case 105: case 106:
                FriendAbility Casey = new Casey();
                player._battleScene.MakeFriend("케이시", Casey, card);
                break;
            case 107: case 108:
                FriendAbility Selmore = new Selmore();
                player._battleScene.MakeFriend("셀모어", Selmore, card);
                player.isDisappearShield = true;
                break;
            case 109: case 110:
                FriendAbility Rudger = new Rudger();
                player._battleScene.MakeFriend("루드거", Rudger, card);
                break;
            case 111: case 112:
                FriendAbility Chellish = new Chellish();
                player._battleScene.MakeFriend("첼리시", Chellish, card);
                break;
            case 113: case 114:
                FriendAbility Esmerilda = new Esmerilda();
                player._battleScene.MakeFriend("에스메릴다", Esmerilda, card);
                break;
            case 115: case 116:
                FriendAbility Line = new Line();
                player._battleScene.MakeFriend("리네", Line, card);
                break;
            case 117: case 118:
                FriendAbility Bian = new Bian();
                player._battleScene.MakeFriend("비안", Bian, card);
                break;
        }
    }
}
