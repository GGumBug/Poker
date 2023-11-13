using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerJudgment
{
    bool isStraight;

    CardData[] currentCards;
    int[] cardnumbers = new int[5];
    HandRank handRank = HandRank.NoPairs;
    List<List<int>> pairList = new();

    public void StartJudgment()
    {
        currentCards = CardManager.instance.CardDatas;

        for (int i = 0; i < currentCards.Length; i++)
            cardnumbers[i] = currentCards[i].Number;
        cardnumbers = InsertionSort(cardnumbers);

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
        if (handRank == HandRank.StraightFlush)
        {
            EndJudgment();
            return;
        }

        CheckPair();
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

        for (int i = 0; i < cardnumbers.Length - 1; i++)
        {
            if (cardnumbers[i] - cardnumbers[i + 1] == -1)
                link++;
            else if (cardnumbers[i] - cardnumbers[i + 1] == -8)
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
        {
            UpDateRank(HandRank.Straight);
            EndJudgment();
        }
        else
            MakePairList();
    }

    void MakePairList()
    {
        for (int i = 0; i < cardnumbers.Length; i++)
        {
            for (global::System.Int32 j = i + 1; j < cardnumbers.Length; j++)
            {
                if (cardnumbers[i] == cardnumbers[j])
                {
                    if (pairList.Count == 0)
                    {
                        AddPairDict(cardnumbers[i]);
                        continue;
                    }

                    int compareIdx = pairList.Count - 1;
                    if (pairList[compareIdx].Contains(cardnumbers[i]))
                        pairList[compareIdx].Add(cardnumbers[i]);
                    else
                        AddPairDict(cardnumbers[i]);
                }
            }
        }

        JudgmentPair();
    }

    void AddPairDict(int key)
    {
        List<int> tempList = new();
        tempList.Add(key);

        pairList.Add(tempList);
    }

    void JudgmentPair()
    {
        int pairListCount = pairList.Count;

        switch(pairListCount)
        {
            case 0:
                UpDateRank(HandRank.NoPairs);
                EndJudgment();
                break;
            case 1:
                CheckFourOfAKind();
                break;
            case 2:
                CheckFullHouse();
                break;
        }
    }

    void CheckFourOfAKind()
    {
        int pairCount = pairList[0].Count;

        switch(pairCount)
        {
            case 1:
                UpDateRank(HandRank.OnePair);
                break;
            case 3:
                UpDateRank(HandRank.ThreeOfAKind);
                break;
            case 6:
                UpDateRank(HandRank.FourOfAKind);
                break;
        }

        EndJudgment();
    }

    void CheckFullHouse()
    {
        foreach (var list in pairList)
        {
            if (list.Count == 3)
            {
                UpDateRank(HandRank.FullHouse);
                EndJudgment();
                return;
            }
        }
        UpDateRank(HandRank.TwoPairs);
        EndJudgment();
    }

    void UpDateRank(HandRank newRank)
    {
        if ((int)handRank < (int)newRank)
            handRank = newRank;

        Debug.Log(handRank.ToString());
    }

    void EndJudgment()
    {
        CardManager.instance.uiPoker.AppearResult(handRank.ToString());
    }

    public void Clear()
    {
        handRank = HandRank.NoPairs;
        isStraight = false;
        pairList.Clear();
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
