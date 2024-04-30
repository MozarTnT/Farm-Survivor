using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
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
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;

        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= 0.1f)
        {
            GameManager.instance.P.data.HP = GameManager.instance.P.data.HP = 100; // 풀피 회복
            float sizeX = ((float)GameManager.instance.P.data.HP / (float)GameManager.instance.P.data.MaxHP) * 120.0f;
            GameManager.instance.P.hpRect.sizeDelta = new Vector2(sizeX, 30.0f);


            Destroy(gameObject);
        }
    }
}
