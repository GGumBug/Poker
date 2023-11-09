using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPoker : MonoBehaviour
{
    public GameObject panelRerollButtons;
    public List<Button> rerollButtons = new();

    private void Awake()
    {
        for (int i = 0; i < rerollButtons.Count; i++)
        {
            int idx = i;
            rerollButtons[i].onClick.AddListener(() => RerollAction(idx));
        }   
    }

    public void RerollButtonActiveCont(int orderNum, bool value)
    {
        rerollButtons[orderNum].gameObject.SetActive(value);
    }

    void RerollAction(int orderNumber)
    {
        rerollButtons[orderNumber].gameObject.SetActive(false);
        CardManager.instance.cards[orderNumber].ReverseCard();
        CardManager.instance.Reroll(orderNumber);
    }
}
