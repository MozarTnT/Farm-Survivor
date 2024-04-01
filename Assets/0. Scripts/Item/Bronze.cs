using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bronze : Exp
{
    void Start()
    {
        transform.SetParent(ItemPooling.Instance.coin_Parent);

        int rand = Random.Range(0, 100);
        if(rand < 33)
        {
            ExpValue = 5.8f;
        }
        else if(rand < 66 && rand >= 33)
        {
            ExpValue = 6.47f;
        }
        else
        {
            ExpValue = 7.35f;
        }
    
    }


}
