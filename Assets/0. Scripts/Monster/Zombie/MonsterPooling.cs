using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterPooling : Singleton<MonsterPooling>
{

    public GameObject rangeObject;
    BoxCollider2D rangeCollider;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider2D>();
    }
}
