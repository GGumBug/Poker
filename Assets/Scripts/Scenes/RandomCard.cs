using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCard : MonoBehaviour
{
    private void Start()
    {
        CardManager.instance.StartPoker();
        //CardManager.instance.Test();
    }
}
