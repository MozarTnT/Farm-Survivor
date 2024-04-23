using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePlayer : MonoBehaviour
{
    private float speed = 3.5f;
    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        float cX = Mathf.Clamp(transform.position.x + x, -10.5f, 10.5f);
        float cY = Mathf.Clamp(transform.position.y + y, -4.3f, 3.3f);

        transform.position = new Vector2(cX, cY);

        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }


}
