using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Exp
{
    void Start()
    {
        transform.SetParent(ItemPooling.Instance.coin_Parent);

        ExpValue = 100;
    }


}
