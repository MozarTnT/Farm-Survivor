using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Exp : MonoBehaviour
{
    protected float ExpValue { get; set; }

    private float speed;

    private Transform target;
    public Transform Target
    { 
        get { return target; } 
        set
        {
            target = value;
            speed = target.GetComponent<Player>().data.Speed * 2f;
        }
    }
    void Update()
    {
        if (target == null)
            return;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

        float distance = Vector2.Distance(transform.position, target.position);
        if(distance <= 0.1f)
        {
            GameObject.FindAnyObjectByType<UI>().topUI.Exp += ExpValue;
            Destroy(gameObject);
        }
    }

    
}
