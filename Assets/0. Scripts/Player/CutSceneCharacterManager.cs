using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCharacterManager : MonoBehaviour
{
    public static CutSceneCharacterManager Instance;

    public bool canMove = false;

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

    void Update()
    {
        
    }
}
