using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
    [SerializeField] private Transform titleTrans;
    [SerializeField] private Transform[] charTrans;

    void Start()
    {
        foreach(var item in charTrans)
        {
            item.gameObject.SetActive(false);
        }

        //int aniIndex = 0;

        titleTrans.DOMoveY(520, 1f)
            .SetDelay(0.5f)
            .SetEase(Ease.InQuad)
            .OnComplete( () => StartCoroutine(CharacterAnimation()) );
    }

    IEnumerator CharacterAnimation()
    {
        Vector2[] aniStartVecs = { new Vector2(-700, 440), new Vector2(-700, 225), new Vector2 (1200,440), new Vector2 (1200,225) };
        Vector2[] aniEndVecs = { new Vector2(390, 0), new Vector2(390, -210), new Vector2(647,-200), new Vector2 (647,10)};

        float delay = 1.5f;

        for (int i = 0; i < charTrans.Length; i++)
        {
            charTrans[i].transform.position = aniStartVecs[i];
            charTrans[i].gameObject.SetActive(true);

            if (aniEndVecs[i].x != 0)
            {
                charTrans[i].DOMoveX(aniEndVecs[i].x, delay)
                    .SetEase(Ease.InQuad);
            }
            else
            {
                charTrans[i].DOMoveX(aniEndVecs[i].y, delay)
                    .SetEase(Ease.InQuad);
            }

            yield return new WaitForSeconds(0.5f);
        }


    }
   
    public void CharSel(int index)
    {
        GameManager.instance.charSelectIndex = index;
        GameManager.instance.OnGame();
    }

    public void HomeBtnOnClicked()
    {
        SceneManager.LoadScene("Title");
    }

 


}
