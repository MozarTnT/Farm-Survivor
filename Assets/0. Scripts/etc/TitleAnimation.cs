using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private Transform topUITrans;
    [SerializeField] private Transform buttonTrans;
    [SerializeField] private Transform cameraTrans;

    private Tween cameraTween;
    void Start()
    {
        Vector3 bigvector3 = new Vector3 (2f, 2f, 2f);
        Vector3 normalvector3 = new Vector3 (1f, 1f, 1f);

        SetAnimation();
        SetCamaraAnimation(10.0f);



    }

    void Update()
    {
        //if (cameraTrans.transform.position.x < -10)
        //{
        //    SetCamaraAnimation();
        //}
   
    }

    void SetAnimation()
    {
        topUITrans.DOMoveY(720, 1.0f).SetDelay(0.3f).SetEase(Ease.InQuad)
            .OnComplete(() => buttonTrans.DOMoveY(70, 1.0f).SetDelay(0.3f).SetEase(Ease.InQuad));
         
         
            //.OnComplete(() => )
            
    }

    void SetShaker()
    {
        topUITrans.DOShakePosition(5.0f, 5.0f).SetDelay(0.2f);
        buttonTrans.DOShakePosition(5.0f, 5.0f).SetDelay(0.2f);
    }

    void SetCamaraAnimation(float targetX)
    {
        cameraTween = cameraTrans.DOMoveX(targetX, 15.0f).SetDelay(0.5f).SetEase(Ease.Linear)
             .OnKill(() => {
                 // 货肺款 Tween 积己
                 if (cameraTrans != null)
                 {
                     SetCamaraAnimation(targetX == 10f ? -10.001f : 10f);
                 }
             });
    }



    private void OnDestroy()
    {
        if(cameraTween != null)
        {
            cameraTween.Kill();
        }
    }


}
