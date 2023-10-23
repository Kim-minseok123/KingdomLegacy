using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Extension;

public class DrawAction : ActionBase
{
    public override void StartAction(PlayerController player, CardData card, EnemyController enemy = null)
    {
        switch (card.ID) {
            case 13: case 14: case 15: case 16: case 59: case 60: case 73: case 74: case 120: case 121:
                player._battleScene.DrawCards(card.drawCard);
                break;
            case 65: case 66:
                player._battleScene.DrawCards(card.drawCard, false);
                break;
            case 71: case 72:
                if (player._battleScene._handCardsUI.Count >= 1) {
                    UI_Card value;
                    value = player._battleScene._handCardsUI.GetRandom();
                    player._battleScene.ExitCard(value);
                }
                player._battleScene.DrawWaitSecond(card.drawCard);

                break;
            case 75: case 76:
                CardData Rancard;
                do {
                    Rancard = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
                } while (Rancard.type != Define.CardType.Attack);
                Rancard.mana = 0;
                player._battleScene.DrawCards(Rancard);
                break;
            case 79:
                CardData attackRandcard;
                do
                {
                    attackRandcard = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
                } while (attackRandcard.type != Define.CardType.Attack || attackRandcard.rarity != Define.CardRarity.Rare);
                player._battleScene.DrawCards(attackRandcard);
                CardData skillRandcard;
                do
                {
                    skillRandcard = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
                } while (skillRandcard.type != Define.CardType.Skill || skillRandcard.rarity != Define.CardRarity.Rare);
                player._battleScene.DrawCards(skillRandcard);
                break;
            case 80:
                CardData attackRandcard2;
                do
                {
                    attackRandcard2 = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
                } while (attackRandcard2.type != Define.CardType.Attack || attackRandcard2.rarity != Define.CardRarity.Unique);
                player._battleScene.DrawCards(attackRandcard2);
                CardData skillRandcard2;
                do
                {
                    skillRandcard2 = Managers.Data.Cards.ElementAt(_rand.Next(0, Managers.Data.Cards.Count)).Value;
                } while (skillRandcard2.type != Define.CardType.Skill || skillRandcard2.rarity != Define.CardRarity.Unique);
                player._battleScene.DrawCards(skillRandcard2);
                break;
        }
    }
}
