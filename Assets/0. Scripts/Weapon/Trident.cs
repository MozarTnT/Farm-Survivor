using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : MonoBehaviour
{
    public float Speed { get; set; }
    public float Power { get; set; }

    Vector2 moveDirection;

    private float lifeTime;
    private float lifeTimeEnd = 0.5f;


    void Start()
    {
        Speed = 20.0f;
        Power = GameManager.instance.P.data.TridentPower;
        SetMoveDirection();
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;

        transform.Translate(moveDirection * Time.deltaTime * Speed);

        lifeTime += Time.deltaTime;
        if(lifeTime >= lifeTimeEnd)
        {
            TridentPooling.Instance.AddpTrident(this);
        }
    }

    void SetMoveDirection()
    {
        // �÷��̾� ĳ������ ������ ������
        Quaternion playerRotation = GameManager.instance.P.transform.rotation;

        Vector2 playerForward;

        if (GameManager.instance.P.transform.localScale.x < 0)
        {
            playerForward = playerRotation * Vector2.up; // ����
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else 
        {
            playerForward = playerRotation * Vector2.up; // ����
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        // �÷��̾� ĳ���Ͱ� �ٶ󺸴� �������� �̵��ϵ��� ���� ����
        moveDirection = playerForward.normalized;

    }

    public void SetPower(float power)
    {
        Power = power;
    }
}
