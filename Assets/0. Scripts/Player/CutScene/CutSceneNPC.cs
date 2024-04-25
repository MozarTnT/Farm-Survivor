using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneNPC : MonoBehaviour
{
    [SerializeField] private List<Sprite> npcIdles;

    [SerializeField] private float idleSpeed;
    public void NPCAniamation()
    {
        GetComponent<SpriteAnimation>().SetSprite(npcIdles, idleSpeed);
    }
    private void OnBecameVisible()
    {
        NPCAniamation();
    }
}
