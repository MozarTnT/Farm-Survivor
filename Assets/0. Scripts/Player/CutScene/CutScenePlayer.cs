using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScenePlayer : MonoBehaviour
{
    [SerializeField] private GameObject key;

    [SerializeField] private List<Sprite> stand;
    [SerializeField] private List<Sprite> run;

    private float speed = 4.5f;

    private bool isNearGate = false;

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
        if (CutSceneCharacterManager.Instance.canMove == true)
        {
            Move();
        }

        if (isNearGate == true && Input.GetKey(KeyCode.B))
        {
            if (key.tag == "Gate")
            {
                HouseSceneManager.Instance.FadeInLoadFarmScene();
            }
            else if (key.tag == "Gate2")
            {
                FarmSceneManager.Instance.FadeInLoadBattleScene();
            }
            else
            {

            }
        }
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

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


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gate"))
        {
            Debug.Log("面倒");
            key.SetActive(true);
            key.GetComponent<KeyAnnounce>().KeyAniamation();
            
            isNearGate = true;
        }

        if (collision.CompareTag("Gate2"))
        {
            Debug.Log("面倒2");
            key.SetActive(true);
            key.GetComponent<KeyAnnounce>().KeyAniamation();

            isNearGate = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gate"))
        {
            Debug.Log("面倒 场");
            key.SetActive(false);
            isNearGate = false;
        }

        if (collision.CompareTag("Gate2"))
        {
            Debug.Log("面倒 场");
            key.SetActive(false);
            isNearGate = false;
        }
    }

   
}
