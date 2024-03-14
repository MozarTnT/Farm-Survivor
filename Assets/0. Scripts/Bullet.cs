using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed { get; set; }
    public int Power { get; set; }
    void Start()
    {
        Speed = 5.0f;
        Power = 10;
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * Speed);
    }
}
