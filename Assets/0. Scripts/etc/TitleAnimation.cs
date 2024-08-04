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

    [SerializeField] private Transform runner1Trans;
    [SerializeField] private Transform runner2Trans;

    private Tween cameraTween;
    void Start()
    {

        Vector3 bigvector3 = new Vector3 (2f, 2f, 2f);
        Vector3 normalvector3 = new Vector3 (1f, 1f, 1f);

        SetAnimation();
        SetCamaraAnimation(10.0f);

        InvokeRepeating("SetRunnerAnimation", 0.0f, 31.0f);

    }


    void SetAnimation()
    {
        topUITrans.DOMoveY(720.0f, 1.5f).SetDelay(0.3f).SetEase(Ease.InQuad);
    }

    void SetRunnerAnimation()
    {
        float targetX = 2500.0f;

        runner1Trans.transform.position = new Vector3(-145f, 80f, -1f);
        runner2Trans.transform.position = new Vector3(-497f, 376f, -1f);

        runner1Trans.DOMoveX(targetX, 15.0f).SetDelay(1.0f).SetEase(Ease.Linear);

        runner2Trans.DOMoveX(targetX, 15.0f).SetDelay(1.2f).SetEase(Ease.Linear);
    
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
