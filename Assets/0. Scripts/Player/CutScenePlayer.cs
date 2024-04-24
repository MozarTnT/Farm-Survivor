using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePlayer : MonoBehaviour
{
    [SerializeField] private List<Sprite> stand;
    [SerializeField] private List<Sprite> run;

    private float speed = 4.5f;

    State state = State.Stand;

    public enum State
    {
        Stand,
        Run,
    }
    void Start()
    {
        GetComponent<SpriteAnimation>().SetSprite(stand, 0.2f);
    }

    void Update()
    {
        if (TalkManager.Instance.canMove == true)
        {
            Move();
        }
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //float cX = Mathf.Clamp(transform.position.x + x, -10.5f, 10.5f);
        //float cY = Mathf.Clamp(transform.position.y + y, -4.3f, 3.3f);
        transform.position = new Vector2(transform.position.x + x, transform.position.y + y);

        if (x < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
        else if (x > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

        if (x == 0 && y == 0 && state != State.Stand)
        {
            state = State.Stand;
            GetComponent<SpriteAnimation>().SetSprite(stand, 0.2f);
        }
        else if ((x != 0 || y != 0) && state != State.Run)
        {
            state = State.Run;
            GetComponent<SpriteAnimation>().SetSprite(run, 0.5f / speed);
        }
    }


}
