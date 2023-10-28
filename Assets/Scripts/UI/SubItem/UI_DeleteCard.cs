using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UI_NonBattleCard;

public class UI_DeleteCard : UI_NonBattleCard
{
    public override bool Init()
    {
        base.Init();
        Image uiImage = GetComponent<Image>();
        uiImage.material = new Material(uiImage.material);
        GetImage((int)Images.CardImage).material = uiImage.material;
        GetImage((int)Images.CardBackGroundImage).material = uiImage.material;
        GetImage((int)Images.CardManaImage).material = uiImage.material;
        GetImage((int)Images.CardRarity).material = uiImage.material;
        return true;
    }
    public override void ClickCard()
    {
        if (Managers.Game.Money < Managers.Game.DeleteCardMoney) return;

        BurnFade();
    }
    public void BurnFade()
    {
        GetText(0).gameObject.SetActive(false);
        GetText(1).gameObject.SetActive(false);
        GetText(2).gameObject.SetActive(false);
        StartCoroutine(Burn());
    }
    IEnumerator Burn()
    {
        Material material = GetComponent<Image>().material;
        material.EnableKeyword("FADE_ON");
        float value = -0.1f;
        while (value <= 1f)
        {
            value += 0.03f;
            material.SetFloat("_FadeAmount", value);
            yield return null;
        }
        material.EnableKeyword("FADE_ON");
        Managers.Game.Money -= Managers.Game.DeleteCardMoney;
        Managers.Game.DeleteCardMoney += 10;
        Managers.Game.Cards.Remove(_cardData.ID);
        Managers.Game.SaveGame();
        Managers.UI.FindPopup<UI_CardDeletePopup>().ShowCardandText();
    }
}
