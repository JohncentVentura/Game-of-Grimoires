using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    [HideInInspector] public RectTransform rectTransform;
    [HideInInspector] public Image outerBorderImage;
    [HideInInspector] public Slider manaSlider;
    [HideInInspector] public Image manaBGImage;
    [HideInInspector] public Image manaImage;
    [HideInInspector] public Image innerBorderImage;
    [HideInInspector] public Image cardBGImage;
    [HideInInspector] public Image cardImage;
    [HideInInspector] public Slider cooldownSlider;
    [HideInInspector] public Image cooldownImage;
    [HideInInspector] public TextMeshProUGUI cooldownText;
    [HideInInspector] public TextMeshProUGUI keyInputText;

    public void InitCardButton(CardData card)
    {
        rectTransform = GetComponent<RectTransform>();
        outerBorderImage = rectTransform.Find("OuterBorderImage").GetComponent<Image>();

        manaSlider = rectTransform.Find("ManaSlider").GetComponent<Slider>();
        manaBGImage = manaSlider.GetComponent<RectTransform>().Find("ManaBGImage").GetComponent<Image>();
        manaImage = manaSlider.GetComponent<RectTransform>().Find("ManaImage").GetComponent<Image>();

        foreach (KeyValuePair<ObjectManager.EManaTypes, string> manaType in ObjectManager.Instance.manaTypes)
        {
            if (card.GetProp(ECardProps.ManaType).value == manaType.Value) manaImage.sprite = ObjectManager.Instance.manaTypeSprites[(int)manaType.Key];
        }

        innerBorderImage = rectTransform.Find("InnerBorderImage").GetComponent<Image>();
        cardBGImage = innerBorderImage.GetComponent<RectTransform>().Find("CardBGImage").GetComponent<Image>();
        cardImage = innerBorderImage.GetComponent<RectTransform>().Find("CardImage").GetComponent<Image>();
        cardImage.sprite = card.cardSprite;

        cooldownSlider = rectTransform.Find("CooldownSlider").GetComponent<Slider>();
        cooldownImage = cooldownSlider.GetComponent<RectTransform>().Find("CooldownImage").GetComponent<Image>();
        cooldownText = cooldownSlider.GetComponent<RectTransform>().Find("CooldownText").GetComponent<TextMeshProUGUI>();

        keyInputText = rectTransform.Find("KeyInputText").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCardData(CardData card)
    {
        manaSlider.maxValue = card.GetStat(ECardStats.Mana).maxValue;
        manaSlider.value = card.GetStat(ECardStats.Mana).value;
        cooldownSlider.maxValue = card.GetStat(ECardStats.Cooldown).maxValue;
        cooldownSlider.value = card.GetStat(ECardStats.Cooldown).value;
    }
    
}