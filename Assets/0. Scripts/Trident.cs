using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : MonoBehaviour
{
    public float Speed { get; set; }
    public float Power { get; set; }

    void Start()
    {
        Speed = 10.0f;
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;

        transform.Translate(Vector2.up * Time.deltaTime * Speed);
        transform.rotation = Quaternion.identity;
    }

    public void SetPower(float power)
    {
        Power = power;
    }
}
