using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed { get; set; }
    public float Power { get; set; }
    void Start()
    {
        transform.SetParent(BulletPooling.Instance.pBulletParent);
    }

    private void OnEnable()
    {
        Speed = 25.0f;
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
        
    }


    public void SetPower(float power)
    {
        Power = power;
    }

}
