using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmSceneManager : MonoBehaviour
{
    public static FarmSceneManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeInLoadBattleScene()
    {
        StartCoroutine(DoFadeInAndLoadBattleScene());
    }

    IEnumerator DoFadeInAndLoadBattleScene()
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn �ڷ�ƾ�� ���� ������ ��ٸ��ϴ�.
        SceneManager.LoadScene("BattleScene");
    }
}
