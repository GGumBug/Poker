using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Transform model;
    MeshFilter filter;

    private void Awake()
    {
        filter = GetComponentInChildren<MeshFilter>();
        model = GetComponentsInChildren<Transform>()[1];
    }

    public void SetCard(CardData cardData)
    {
        string shape = cardData.Shape.ToString();
        string num = cardData.Number.ToString();

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
        model.Rotate(new Vector3(0, 180f,0));
    }
}
