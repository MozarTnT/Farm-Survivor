using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCharacterManager : MonoBehaviour
{
    public static CutSceneCharacterManager Instance;

    public bool canMove = false; // 이동 통제용 boll

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
