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

        // ���� ����
        pos.x = Mathf.Clamp(pos.x, -11f, 11f);
        pos.y = Mathf.Clamp(pos.y, -15f, 15f);

        // Ÿ���� Ÿ���� ��ġ�� ���� ����ٴϱ�
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5f);

    }
}
