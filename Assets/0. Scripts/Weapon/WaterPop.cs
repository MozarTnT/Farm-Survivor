using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WaterPop : MonoBehaviour
{
    [SerializeField] private List<Sprite> waters;

    private float waterLifeTimer = 0;
    private float waterLife = 5.0f;

    //Collider2D capsuleCollider = new CapsuleCollider2D();

    void Start()
    {
        transform.position = Monster.waterPopVec;
        WaterPopAniamation();

        //capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        waterLifeTimer += Time.deltaTime;

        if (waterLifeTimer >= waterLife ) 
        {
            Destroy(gameObject);
        }

        
        //if (capsuleCollider != null)
        //{
        //    if (capsuleCollider.CompareTag("WaterPop") && gameObject.GetComponent<CapsuleCollider2D>().IsTouching(capsuleCollider))
        //    {
        //        Destroy(gameObject);
        //    }
        //    else
        //    {

        //    }
        //}
        //else
        //{
        //    return;
        //}
    }

    public void WaterPopAniamation()
    {
        GetComponent<SpriteAnimation>().SetSprite(waters, 0.1f);
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.CompareTag("WaterPop"))
    //    {
    //        //capsuleCollider = other;
    //        Debug.Log(other.tag.ToString());
    //        Destroy(other.gameObject);
    //    }
    //}

}