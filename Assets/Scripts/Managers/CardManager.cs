using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    
    private CardShuffler shuffler;
    public UIPoker uiPoker;

    public List<Card> cards = new();
    CardData[] cardDatas = new CardData[5];

    private void Awake()
    {
        instance = this;
        shuffler = gameObject.AddComponent<CardShuffler>();

        for (int i = 0; i < cards.Count; i++)
            cards[i].OrderNumber = i;
    }

    public void StartPoker()
    {
        shuffler.MakeDeck();
    }

    public void AddSelectedCard(int orderNumber, CardData cardData)
    {
        cardDatas[orderNumber] = cardData;
    }

    public void ExportCardData(int orderNumber, bool isReroll = false)
    {
        cards[orderNumber].SetCard(cardDatas[orderNumber], isReroll);
    }

    public void Reroll(int orderNumber)
    {
        shuffler.RerollCard(orderNumber);
    }
}
