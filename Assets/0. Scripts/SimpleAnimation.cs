using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float delay;

    private Image img;
    private int index = 0;
    private float timer = 0;

    
  

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= delay)
        {
            timer = 0;
            index++;
            if(index >= sprites.Length)
            {
                index = 0;
            }

            img.sprite = sprites[index];
        }
    }

    void OnDeSelect()
    {
        
    }
}
