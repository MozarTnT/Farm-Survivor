using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFollowCam : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        Vector3 pos = target.position;
        pos.z = -10;

        // ���� ����
        pos.x = Mathf.Clamp(pos.x, -13.8f, 15.1f);
        pos.y = Mathf.Clamp(pos.y, -16.8f, 19.6f);

        // Ÿ���� Ÿ���� ��ġ�� ���� ����ٴϱ�
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 5f);

    }
}
