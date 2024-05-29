using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CutSceneFollowCam : MonoBehaviour
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
        pos.x = Mathf.Clamp(pos.x, -10.35f, 11.5f);
        pos.y = Mathf.Clamp(pos.y, -5.97f, 5.97f);

        // 타겟을 타겟의 위치에 따라 따라다니기
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5f);

    }
}
