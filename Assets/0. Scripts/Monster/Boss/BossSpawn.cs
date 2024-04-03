using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    float spawnTimer;
    float spawnDelay;

    [SerializeField] private Player player;
    [SerializeField] private Exp[] exps;
    [SerializeField] private KingSlime ks;

    private bool slimeSpawn = false;
    void Start()
    {
        spawnDelay = Random.Range(1, 3);
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;

        spawnTimer += Time.deltaTime;

        if (GameManager.instance.UI.topUI.Level == 10 && !slimeSpawn)
        {
            slimeSpawn = true;
            Debug.Log(GameManager.instance.UI.topUI.Level);
            SpawnKS();

            GameManager.instance.UI.uiAnimation.gameObject.SetActive(true);
            GameManager.instance.UI.bossGameObject.gameObject.SetActive(true);
            GameManager.instance.UI.bossHP.gameObject.SetActive(true);
        }


    }

    // ���� ���� 
    // ������ ����� Plane�� �ڽ��� RespawnRange ������Ʈ
    public GameObject rangeObject;
    Collider2D rangeCollider;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<Collider2D>();
    }


    public void SpawnKS()
    {
        KingSlime kingSlime = Instantiate(ks, Return_RandomPosition(), Quaternion.identity);
        kingSlime.SetTarget(player.transform);
        kingSlime.SetExp(exps);
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);

        return originPosition + new Vector3(range_X, range_Y, 0f);
    }
}
