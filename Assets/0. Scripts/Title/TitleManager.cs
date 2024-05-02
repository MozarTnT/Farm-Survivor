using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;

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

    public void FadeInLoadHouseScene()
    {
        StartCoroutine(DoFadeInAndLoadFarmScene());
    }

    IEnumerator DoFadeInAndLoadFarmScene()
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn �ڷ�ƾ�� ���� ������ ��ٸ��ϴ�.
        SceneManager.LoadScene("HouseScene");
    }

    public void FadeInLoadCharSelectScene()
    {
        StartCoroutine(DoFadeInAndLoadCharSelectScene());
    }

    IEnumerator DoFadeInAndLoadCharSelectScene()
    {
        yield return StartCoroutine(Fader.Instance.FadeIn()); // FadeIn �ڷ�ƾ�� ���� ������ ��ٸ��ϴ�.
        SceneManager.LoadScene("CharacterSelect");
    }
}