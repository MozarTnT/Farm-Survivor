using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnnounce : MonoBehaviour
{
    [SerializeField] private List<Sprite> coins;

    public void KeyAniamation()
    {
        GetComponent<SpriteAnimation>().SetSprite(coins, 0.2f);
    }
}
