using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    CardColor color = CardColor.Black;
    CardShape shape = CardShape.Club;
    int number = 0;

    public CardColor Color => color;
    public CardShape Shape => shape;
    public int Number => number;

    public CardData(CardColor cardColor, CardShape cardShape, int numder)
    {
        color = cardColor;
        shape = cardShape;
        this.number = numder;
    }
}
