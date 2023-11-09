using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    
    CardShuffler shuffler;

    public List<Card> cards = new();
    List<CardData> cardDatas = new();

    private void Awake()
    {
        instance = this;
        shuffler = gameObject.AddComponent<CardShuffler>();
    }

    public void StartPoker()
    {
        shuffler.MakeDeck();
    }

    public void AddSelectedCard(CardData cardData)
    {
        cardDatas.Add(cardData);
    }

    public void ExportCardData()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetCard(cardDatas[i]);
        }
    }
}
