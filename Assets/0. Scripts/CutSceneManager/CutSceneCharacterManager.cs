using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCharacterManager : MonoBehaviour
{
    public static CutSceneCharacterManager Instance; // 인스턴스

    public bool canMove = false; // 이동 가능 여부

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 인스턴스 설정
            DontDestroyOnLoad(gameObject); // 씬 전환 시 오브젝트 유지
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }
}
