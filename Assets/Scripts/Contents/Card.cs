using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    int orderNumber;

    CardData curData;
    Transform model;
    MeshFilter filter;
    WaitForSeconds waitTime = new(2f);

    public int OrderNumber { get { return orderNumber; } set { orderNumber = value; } }

    private void Awake()
    {
        filter = GetComponentInChildren<MeshFilter>();
        model = GetComponentsInChildren<Transform>()[1];
    }

    public void SetCard(CardData cardData, bool isReroll)
    {
        curData = cardData;
        string shape = curData.Shape.ToString();
        string num = curData.Number.ToString();

        switch (num) 
        {
            case "1":
                num = "Ace";
                break;
            case "10":
                num = "Jack";
                break;
            case "11":
                num = "Queen";
                break;
            case "12":
                num = "King";
                break;
        }

        filter.mesh = Resources.Load<Mesh>($"Meshs/{shape}_{num}");

        StartCoroutine(ReverseAnim(isReroll));
    }

    IEnumerator ReverseAnim(bool isReroll)
    {
        model.Rotate(Vector3.zero);
        model.DORotate(new Vector3(0, -180f, 0), 2f);

        yield return waitTime;

        if (!isReroll)
            CardManager.instance.uiPoker.RerollButtonActiveCont(OrderNumber, true);

        CardManager.instance.uiPoker.ActiveTrueCheckButton();
    }

    public void ReverseCard()
    {
        model.localEulerAngles = Vector3.zero;
    }
}
