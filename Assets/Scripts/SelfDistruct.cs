using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDistruct : MonoBehaviour
{
    // in enemy explosion (prefab)
    [SerializeField] float timeTillDestroy = 2f;
    
    void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}
