using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] private Transform slimeTrans;
    [SerializeField] private Transform slimeTextTrans;


    void Start()
    {
        SlimeAction();
    }

    void Update()
    {
        
    }

    private void SlimeAction()
    {
        GameManager.instance.state = GameState.Stop;
        slimeTrans.DOMoveX(2500, 1.5f).SetDelay(0.7f).SetEase(Ease.InQuad);
        slimeTextTrans.DOMoveX(-2000, 2.0f).SetDelay(0.7f).SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                GameManager.instance.state = GameState.Play;
                GameManager.instance.UI.uiAnimation.gameObject.SetActive(false);
                GameManager.instance.UI.bossGameObject.gameObject.SetActive(false);
            });

    }


}
