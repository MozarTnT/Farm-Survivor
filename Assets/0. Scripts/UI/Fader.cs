using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public static Fader Instance;

    private Image panel;
    private Color startColor = Color.black;
    private Color endColor = Color.clear;
    private float fadeDuration = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        panel = GetComponentInChildren<Image>();
        panel.color = endColor;
    }


    public IEnumerator FadeOut()
    {
        Debug.Log("Fade Out");
        float timer = 0.0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            panel.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        float timer = 0.0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            panel.color = Color.Lerp(endColor, startColor, timer / fadeDuration);
            yield return null;
        }
    }

    public IEnumerator FadeInOut()
    {
        yield return StartCoroutine(FadeIn());
        yield return StartCoroutine(FadeOut());
    }



}


