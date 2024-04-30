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
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn 코루틴이 끝날 때까지 기다립니다.
        SceneManager.LoadScene("BattleScene");
    }
}
