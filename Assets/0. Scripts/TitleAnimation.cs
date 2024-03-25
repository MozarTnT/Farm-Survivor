using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private Transform titleTrans;
    [SerializeField] private Transform startTrans;
    void Start()
    {
        Vector3 bigvector3 = new Vector3 (2f, 2f, 2f);
        Vector3 normalvector3 = new Vector3 (1f, 1f, 1f);

        SetAnimation();
            
            
        
    }

    void Update()
    {


    }

    void SetAnimation()
    {
        titleTrans.DOMoveY(630, 1.0f).SetDelay(0.3f).SetEase(Ease.InQuad)
            .OnComplete(() => startTrans.DOMoveY(150, 1.0f).SetDelay(0.3f).SetEase(Ease.InQuad));
         
         
            //.OnComplete(() => )
            
    }

    void SetShaker()
    {
        titleTrans.DOShakePosition(5.0f, 5.0f).SetDelay(0.2f);
        startTrans.DOShakePosition(5.0f, 5.0f).SetDelay(0.2f);
    }


}
