using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCharacterManager : MonoBehaviour
{
    public static CutSceneCharacterManager Instance;

    public bool canMove = false;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        
    }
}
