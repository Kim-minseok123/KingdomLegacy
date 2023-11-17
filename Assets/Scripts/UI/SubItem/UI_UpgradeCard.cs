using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeCard : UI_NonBattleCard
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
        GetImage((int)Images.Black).material = uiImage.material;

        return true;
    }
    public override void ClickCard()
    {
        if (Managers.UI.FindPopup<UI_EnhancePopup>().isClickUpgradeCard) return;
       Managers.UI.FindPopup<UI_EnhancePopup>().isClickUpgradeCard = true;
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
        Material material = GetComponent<Image>().materialForRendering;
        material.EnableKeyword("FADE_ON");
        float value = -0.1f;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/¼Ò¸ê", Managers.Game.EffectSound);

        while (value <= 1f)
        {
            value += 0.02f;
            material.SetFloat("_FadeAmount", value);
            yield return null;
        }
        material.DisableKeyword("FADE_ON");
        Managers.Game.Cards.Remove(_cardData.ID);
        Managers.Data.Cards.TryGetValue(_cardData.ID + 1, out _cardData);
        RefreshUI();
        material.EnableKeyword("FADE_ON");
        value = 1f;
        Managers.Sound.Play(Define.Sound.Effect, "Effect/°­È­", Managers.Game.EffectSound);

        while (value >= 0f)
        {
            value -= 0.02f;
            material.SetFloat("_FadeAmount", value);
            yield return null;
        }
        material.DisableKeyword("FADE_ON");
        GetText(0).gameObject.SetActive(true);
        GetText(1).gameObject.SetActive(true);
        GetText(2).gameObject.SetActive(true);

        Managers.Game.Cards.Add(_cardData.ID);
        yield return new WaitForSeconds(0.5f);
        Managers.UI.FindPopup<UI_RestOrEnhancePopup>().Enhance();
        Managers.UI.FindPopup<UI_EnhancePopup>().ExitButton();
    }
}
