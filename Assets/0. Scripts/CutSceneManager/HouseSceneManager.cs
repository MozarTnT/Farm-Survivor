using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseSceneManager : MonoBehaviour
{
    public static HouseSceneManager Instance;

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


    public void FadeInLoadFarmScene()
    {
        StartCoroutine(DoFadeInAndLoadFarmScene());
    }

    IEnumerator DoFadeInAndLoadFarmScene()
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn 코루틴이 끝날 때까지 기다립니다.
        SceneManager.LoadScene("FarmScene");
    }

}
