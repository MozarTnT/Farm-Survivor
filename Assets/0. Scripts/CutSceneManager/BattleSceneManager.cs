using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{

    Vector3 playerVector = Vector3.zero;
    void Start()
    {
        Camera.main.transform.position = new Vector3(-13.8f, 0.2f, -10f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Camera.main.transform.position = new Vector3(-13.8f, 0.2f, -10f);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Camera.main.transform.position = new Vector3(14.83f, 0.2f, -10f);

        }
    }
}
