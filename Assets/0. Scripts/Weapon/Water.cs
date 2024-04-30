using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private List<Sprite> waters;

    public float waterSpeed = 10f; // 초기 속도
    public float angle = 45f; // 이동 방향의 각도 (degrees)
    public float duration = 0.5f; // 포물선 운동의 지속 시간
    public float stopDuration = 1f; // 멈춰 있는 시간

    private Vector3 initialPosition;

    private float waterLifeTimer = 0;
    private float waterLife = 1.0f;
 
    void Start()
    {
        Quaternion quaternion = GameManager.instance.P.waterPos.transform.rotation;
        transform.position = Player.Instance.waterPos.position; // waterPos 임의로 지정
        transform.rotation = quaternion;

        WaterAniamation();
        initialPosition = transform.position;
    }

    void Update()
    {
        Move();

        waterLifeTimer += Time.deltaTime;

        if (waterLifeTimer >= waterLife)
        {
            Destroy(gameObject);
        }
    }


    public void Move()
    {
        Quaternion quaternion = GameManager.instance.P.waterPos.transform.rotation;
        transform.Translate(Vector3.up * Time.deltaTime * waterSpeed);
    }


    public void WaterAniamation()
    {
        GetComponent<SpriteAnimation>().SetSprite(waters, 0.1f);
    }
}
