using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;



public class DamageText : MonoBehaviour
{
    public float speed;
    public float alphaSpeed;
    public float destroyTime;
    TextMeshPro text;
    
    Color alpha;
    public float damage;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString("F0");
        alpha = text.color;
        Invoke("DestroyObject", destroyTime);
    }

   
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
