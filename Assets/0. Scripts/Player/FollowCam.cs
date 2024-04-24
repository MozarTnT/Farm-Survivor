using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = target.position;
        pos.z = -10;

        // 범위 지정
        pos.x = Mathf.Clamp(pos.x, -8.55f, 9.7f);
        pos.y = Mathf.Clamp(pos.y, -13.9f, 16.8f);

        // 타겟을 타겟의 위치에 따라 따라다니기
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5f);

    }
}
