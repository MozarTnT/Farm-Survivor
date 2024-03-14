using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Stand,
        Run,
        Dead,
    }

    State state = State.Stand;

    [SerializeField] private List<Sprite> stand;
    [SerializeField] private List<Sprite> run;
    [SerializeField] private List<Sprite> dead;

    [SerializeField] private Transform firePos;

    [SerializeField] private float speed = 5.0f;



    public Transform target;

    void Start()
    {
        GetComponent<SpriteAnimation>().SetSprite(stand, 0.1f);
    }

    void Update()
    {
        if (target == null)
            return;

        Move();
        FireRotate();
        Fire();
    }

    private void Move()
    {
        // Move
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        float cX = Mathf.Clamp(transform.position.x + x, -19f, 19f);
        float cY = Mathf.Clamp(transform.position.y + y, -19.2f, 19.4f);

        transform.position = new Vector2(cX, cY);

        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x > 0)
        {
            transform.localScale = Vector3.one;
        }

        // Animation
        if (x == 0 && y == 0 && state != State.Stand)
        {
            state = State.Stand;
            GetComponent<SpriteAnimation>().SetSprite(stand, 0.1f);
        }
        else if ((x != 0 || y != 0) && state != State.Run)
        {
            state = State.Run;
            GetComponent<SpriteAnimation>().SetSprite(run, 1 / speed);
        }



    }

    private void FireRotate()
    {
        // 타켓을 찾아 방향 전환
        Vector2 vec = transform.position - target.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }


    private float fireTimer = 0;
    [SerializeField] private float fireDelay;
    [SerializeField] private Bullet bullet;
    private void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireDelay)
        {
            fireTimer = 0;
            Instantiate(bullet, firePos.GetChild(0)).transform.SetParent(null);
        }
    }
}
