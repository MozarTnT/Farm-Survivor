using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmSceneManager : MonoBehaviour
{
    public static FarmSceneManager Instance; // 인스턴스

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 인스턴스 설정
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    public void FadeInLoadBattleScene() // 배틀 씬 로드
    {
        StartCoroutine(DoFadeInAndLoadBattleScene()); // 코루틴 시작
    }

    IEnumerator DoFadeInAndLoadBattleScene() // 배틀 씬 로드 코루틴
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // 페이드 인
        SceneManager.LoadScene("BattleScene"); // 배틀 씬 로드
    }
}
