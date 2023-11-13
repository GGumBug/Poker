using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPoker : MonoBehaviour
{
    public GameObject panelRerollButtons;
    public Button btnCheck;
    public Button btnClear;
    public TMP_Text txtResult;
    public List<Button> rerollButtons = new();

    Tweener tweener;

    private void Awake()
    {
        btnCheck.onClick.AddListener(StartJudgment);
        btnClear.onClick.AddListener(Clear);

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

    public void ActiveTrueCheckButton()
    {
        if (!btnCheck.gameObject.activeSelf)
        {
            btnCheck.gameObject.SetActive(true);
        }
    }

    void StartJudgment()
    {
        btnCheck.gameObject.SetActive(false);
        CardManager.instance.judgment.StartJudgment();
    }

    public void AppearResult(string result)
    {
        tweener = txtResult.DOFade(1, 2f);
        txtResult.text = result;
    }

    void Clear()
    {
        tweener.Kill();
        txtResult.DOFade(0, 0.5f);
        foreach (var btn in rerollButtons)
            btn.gameObject.SetActive(false);
        CardManager.instance.Clear();
    }
}
