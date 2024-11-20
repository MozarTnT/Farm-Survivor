using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{

    public enum State
    { 
        Idle,
        Walk,
        Hit,
        Dead,
    
    }

    [System.Serializable]
    public class Data
    {
        public int Level { get; set; }
        public int HP { get; set; }
        public int Power { get; set; }
        public int Defence { get; set; }
        public float Speed { get; set; }
        public float HitDelay { get; set; }
        public float AttDelay { get; set; }
    }

    protected Data data = new Data();

    public Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.state != GameState.Play)
            return;


        if (target == null || data.HP <= 0)
            return;


    }


}
