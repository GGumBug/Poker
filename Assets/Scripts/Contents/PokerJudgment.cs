using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerJudgment
{
    bool isOnePair;
    bool isThreePair;
    bool isStraight;
    bool isFlush;

    CardData[] currentCards;
    int[] Cardnumbers = new int[5];
    HandRank handRank = HandRank.NoPairs;

    public void StartJudgment()
    {
        currentCards = CardManager.instance.CardDatas;

        for (int i = 0; i < currentCards.Length; i++)
            Cardnumbers[i] = currentCards[i].Number;
        Cardnumbers = InsertionSort(Cardnumbers);

        CheckFlush();
    }

    void CheckFlush()
    {
        CardShape cardShape = currentCards[0].Shape;

        for (int i = 1; i < currentCards.Length; i++)
        {
            if (cardShape != currentCards[i].Shape)
            {
                CheckPair();
                return;
            }
            else
                cardShape = currentCards[i].Shape;
        }

        UpDateRank(HandRank.Flush);
        CheckStraightFlush();
        
        CheckPair(); // 풀하우스나 포카드 체크로직 나오면 교체
    }

    void CheckStraightFlush()
    {
        CheckStraight();
        if (isStraight)
            UpDateRank(HandRank.StraightFlush);
    }

    void CheckStraight()
    {
        int link = 0;
        bool isEight = false;

        for (int i = 0; i < Cardnumbers.Length - 1; i++)
        {
            if (Cardnumbers[i] - Cardnumbers[i + 1] == -1)
                link++;
            else if (Cardnumbers[i] - Cardnumbers[i + 1] == -8)
                isEight = true;
        }

        if (link == 4)
            isStraight = true;
        else if (link >= 3 && isEight)
            isStraight = true;
    }

    void CheckPair()
    {
        CheckStraight();
        if (isStraight)
            UpDateRank(HandRank.Straight);
    }   

    void UpDateRank(HandRank newRank)
    {
        if ((int)handRank < (int)newRank)
            handRank = newRank;

        Debug.Log(handRank.ToString());
    }

    private int[] InsertionSort(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }

        return arr;
    }
}
