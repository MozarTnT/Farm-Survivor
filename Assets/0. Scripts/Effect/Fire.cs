using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private List<Sprite> fires;

    private void OnBecameVisible()
    {
        FireAnimation();
    }
    private void OnBecameInvisible()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void FireAnimation()
    {   
        if (GetComponent<SpriteRenderer>().enabled == false)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        GetComponent<SpriteAnimation>().SetSprite(fires, 0.2f);
    }


}
