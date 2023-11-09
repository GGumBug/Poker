using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    
    private CardShuffler shuffler;
    public PokerJudgment judgment;
    public UIPoker uiPoker;

    public List<Card> cards = new();
    public CardData[] CardDatas { get; private set; } = new CardData[5];

    private void Awake()
    {
        instance = this;

        shuffler = gameObject.AddComponent<CardShuffler>();
        judgment = new PokerJudgment();

        for (int i = 0; i < cards.Count; i++)
            cards[i].OrderNumber = i;
    }

    public void StartPoker()
    {
        shuffler.MakeDeck();
    }

    public void AddSelectedCard(int orderNumber, CardData cardData)
    {
        CardDatas[orderNumber] = cardData;
    }

    public void ExportCardData(int orderNumber, bool isReroll = false)
    {
        cards[orderNumber].SetCard(CardDatas[orderNumber], isReroll);
    }

    public void Reroll(int orderNumber)
    {
        shuffler.RerollCard(orderNumber);
    }

    //TestLogic
    public void Test()
    {
        CardDatas[0] = new CardData(CardColor.Red, CardShape.Heart, 1);
        CardDatas[1] = new CardData(CardColor.Red, CardShape.Heart, 2);
        CardDatas[2] = new CardData(CardColor.Red, CardShape.Heart, 3);
        CardDatas[3] = new CardData(CardColor.Red, CardShape.Heart, 4);
        CardDatas[4] = new CardData(CardColor.Red, CardShape.Heart, 5);
    }
}
