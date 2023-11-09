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
    }

    void CheckFlush()
    {
        CardShape cardShape = currentCards[0].Shape;

        for (int i = 1; i < currentCards.Length; i++)
        {
            if (cardShape != currentCards[i].Shape)
            {
                // 페어 계산으로 넘어가기
            }
            else
                cardShape = currentCards[i].Shape;
        }

        isFlush = true;
        CheckStraightFlush();
    }

    void CheckStraightFlush()
    {
        CheckStraight();
        if (isStraight)
            CheckRank(HandRank.StraightFlush);
        else
            isFlush = true;
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

    }   

    void CheckRank(HandRank newRank)
    {
        if ((int)handRank < (int)newRank)
            handRank = newRank;
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
