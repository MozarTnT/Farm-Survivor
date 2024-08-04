using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
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

    public void FadeInLoadCharSelectScene()
    {
        StartCoroutine(DoFadeInAndLoadCharSelectScene());
    }

    IEnumerator DoFadeInAndLoadCharSelectScene()
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn 코루틴이 끝날 때까지 기다립니다.
        SceneManager.LoadScene("CharacterSelect");
    }

}
