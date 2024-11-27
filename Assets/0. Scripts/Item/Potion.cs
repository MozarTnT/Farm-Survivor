using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private float speed; // 이동 속도

    private Transform target; // 목표 변환
    public Transform Target
    {
        get { return target; }
        set
        {
            target = value; // 목표 설정
            speed = target.GetComponent<Player>().data.Speed * 2f; // 속도 계산
        }
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return; // 게임 상태 확인

        if (target == null)
            return; // 목표가 없으면 종료

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed); // 목표로 이동

        float distance = Vector2.Distance(transform.position, target.position); // 거리 계산

        if (distance <= 0.1f) // 목표에 도달했을 때
        {
            GameManager.instance.P.data.HP = 100; // HP 회복
            float sizeX = ((float)GameManager.instance.P.data.HP / (float)GameManager.instance.P.data.MaxHP) * 120.0f; // HP 바 크기 계산
            GameManager.instance.P.hpRect.sizeDelta = new Vector2(sizeX, 30.0f); // HP 바 크기 설정

            Destroy(gameObject); // 오브젝트 파괴
        }
    }
}
