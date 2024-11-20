using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseSceneManager : MonoBehaviour
{
    public static HouseSceneManager Instance; // 인스턴스

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

    public void FadeInLoadFarmScene() // 농장 씬 로드
    {
        StartCoroutine(DoFadeInAndLoadFarmScene()); // 코루틴 시작
    }

    IEnumerator DoFadeInAndLoadFarmScene() // 농장 씬 로드 코루틴
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // 페이드 인
        SceneManager.LoadScene("FarmScene"); // 농장 씬 로드
    }
}
