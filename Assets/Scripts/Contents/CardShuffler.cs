using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShuffler : MonoBehaviour
{
    List<CardData> originDeck = new();
    List<CardData> remainDeck = new();

    public void MakeDeck()
    {
        for (int i = 0; i < 4; i++)
        {
            CardColor cardColor = (CardColor)(i / 2);
            CardShape cardShape = (CardShape)i;

            for (global::System.Int32 j = 1; j < 13; j++)
            {
                CardData cardData = new CardData(cardColor, cardShape, j);
                originDeck.Add(cardData);
            }
        }

        SetCurrentFiveCard();
    }

    void SetCurrentFiveCard()
    {
        foreach (var card in originDeck)
        {
            CardData cloneCard = card;
            remainDeck.Add(cloneCard);
        }

        int maxCount = remainDeck.Count;
        for (int i = 0; i < 5; i++)
        {
            int ran = Random.Range(0, maxCount);
            CardManager.instance.AddSelectedCard(remainDeck[ran]);
            remainDeck[ran] = remainDeck[maxCount - 1];
            maxCount--;
        }

        CardManager.instance.ExportCardData();
    }

    public void DrawCard(int count)
    {

    }
}
