using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneZombie : MonoBehaviour
{
    [SerializeField] private List<Sprite> zombieWalking;

    [SerializeField] private float idleSpeed;

    public void Start()
    {
        transform.localScale = new Vector3 (-1.5f, 1.5f, 1.5f);
    }
    public void ZombieAniamation()
    {
        GetComponent<SpriteAnimation>().SetSprite(zombieWalking, idleSpeed);
    }
    private void OnBecameVisible()
    {
        ZombieAniamation();
    }
}
