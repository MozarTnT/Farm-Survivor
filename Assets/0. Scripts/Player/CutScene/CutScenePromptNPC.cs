using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePromptNPC : MonoBehaviour
{
    [SerializeField] private List<Sprite> stand;

    void Start()
    {
        GetComponent<SpriteAnimation>().SetSprite(stand, 0.2f);
    }

    void Update()
    {
        
    }
}
