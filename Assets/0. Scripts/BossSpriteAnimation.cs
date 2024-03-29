using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]

public class BossSpriteAnimation : MonoBehaviour
{
    List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;
    int index = 0;
    float delay = 0f;
    float timer = 0f;

    float endTime = -1.0f;
    float endTimer = 0f;
    UnityAction action;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (sprites.Count == 0)
            return;

        timer += Time.deltaTime;
        if (timer >= delay)
        {
            timer = 0f;
            index++;

            if (index >= sprites.Count)
            {
                index = 0;
            }

            sr.sprite = sprites[index];

        }

        if (endTime != -1)
        {
            endTimer += Time.deltaTime;
            if (endTimer >= endTime)
            {
                endTime = -1.0f;
                action.Invoke();
                action = null;
            }
        }

    }

    void Init()
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        index = 0;
        timer = 0f;
        endTimer = 0f;
        endTime = -1.0f;
        action = null;
    }

    public void SetSprite(List<Sprite> sprites, float delay)
    {
        Init();
        this.sprites = sprites;
        this.delay = delay;
        sr.sprite = sprites[0];
    }
    public void SetSprite(List<Sprite> sprites, float delay, float endTime, UnityAction action)
    {
        Init();
        this.sprites = sprites;
        this.delay = delay;
        this.endTime = endTime;
        this.action = action;
        sr.sprite = sprites[0];
    }
}
