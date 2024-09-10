using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanelController : MonoBehaviour
{
    private PlayerData playerData;

    [Header("UI")]
    [HideInInspector] RectTransform rectTransform;
    [HideInInspector] public List<CardButton> cardButtons;

    private void Start()
    {
        playerData = PlayerManager.Instance.playerData;
        rectTransform = GetComponent<RectTransform>();
        cardButtons = new List<CardButton>();

        for (int i = 0; i < playerData.deckSize; i++)
        {
            cardButtons.Insert(i, rectTransform.Find("CardButton (" + i + ")").GetComponent<CardButton>());
            cardButtons[i].InitCardButton(playerData.activeDeck[i]);
            cardButtons[i].UpdateCardData(playerData.activeDeck[i]);
        }
    }

    private void Update()
    {   
        for (int i = 0; i < playerData.deckSize; i++)
        {
            cardButtons[i].UpdateCardData(playerData.activeDeck[i]);

            //CardButtons ManaSlider value
            cardButtons[i].manaSlider.value = playerData.activeDeck[i].GetStat(ECardStats.Mana).value;

            //CardButtons CooldownSlider value
            cardButtons[i].cooldownSlider.value = playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value;

            //CardButtons CooldownText
            if (playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value > 0)
            {
                cardButtons[i].cooldownText.text = "" + playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value.ToString("F1"); //F1 means 1 decimal is shown
            }
            else
            {
                cardButtons[i].cooldownText.text = "";
            }

            
            //CardButtons Transparency
            if (playerData.activeDeck[i].GetStat(ECardStats.Mana).value < playerData.activeDeck[i].GetStat(ECardStats.Mana).maxValue || playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value > 0)
            {
                cardButtons[i].outerBorderImage.color = new Color(cardButtons[i].outerBorderImage.color.r, cardButtons[i].outerBorderImage.color.g, cardButtons[i].outerBorderImage.color.b, 0.25f);
                cardButtons[i].manaBGImage.color = new Color(cardButtons[i].manaBGImage.color.r, cardButtons[i].manaBGImage.color.g, cardButtons[i].manaBGImage.color.b, 0.25f);
                cardButtons[i].manaImage.color = new Color(cardButtons[i].manaImage.color.r, cardButtons[i].manaImage.color.g, cardButtons[i].cardImage.color.b, 0.25f);
                cardButtons[i].innerBorderImage.color = new Color(cardButtons[i].innerBorderImage.color.r, cardButtons[i].innerBorderImage.color.g, cardButtons[i].innerBorderImage.color.b, 0.25f);
                cardButtons[i].cardBGImage.color = new Color(cardButtons[i].cardBGImage.color.r, cardButtons[i].cardBGImage.color.g, cardButtons[i].cardBGImage.color.b, 0.25f);
                cardButtons[i].cardImage.color = new Color(cardButtons[i].cardImage.color.r, cardButtons[i].cardImage.color.g, cardButtons[i].cardImage.color.b, 0.25f);
            }
            else if (playerData.activeDeck[i].GetStat(ECardStats.Mana).value >= playerData.activeDeck[i].GetStat(ECardStats.Mana).maxValue && playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value <= 0)
            {
                cardButtons[i].outerBorderImage.color = new Color(cardButtons[i].outerBorderImage.color.r, cardButtons[i].outerBorderImage.color.g, cardButtons[i].outerBorderImage.color.b, 1f);
                cardButtons[i].manaBGImage.color = new Color(cardButtons[i].manaBGImage.color.r, cardButtons[i].manaBGImage.color.g, cardButtons[i].manaBGImage.color.b, 1f);
                cardButtons[i].manaImage.color = new Color(cardButtons[i].manaImage.color.r, cardButtons[i].manaImage.color.g, cardButtons[i].cardImage.color.b, 1f);
                cardButtons[i].innerBorderImage.color = new Color(cardButtons[i].innerBorderImage.color.r, cardButtons[i].innerBorderImage.color.g, cardButtons[i].innerBorderImage.color.b, 1f);
                cardButtons[i].cardBGImage.color = new Color(cardButtons[i].cardBGImage.color.r, cardButtons[i].cardBGImage.color.g, cardButtons[i].cardBGImage.color.b, 1f);
                cardButtons[i].cardImage.color = new Color(cardButtons[i].cardImage.color.r, cardButtons[i].cardImage.color.g, cardButtons[i].cardImage.color.b, 1f);
            }
            
        }
    }

}