using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        // we have just one ScoreBoard
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddRigidbody();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void AddRigidbody()
    {
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        // gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        // GetComponent<Rigidbody>().useGravity = false;
    }

    // Player's laser collision on
    // OnParticleCollision is called when a particle hits a Collider.
    // This can be used to apply damage to a GameObject when hit by particles. --> laser
    void OnParticleCollision(GameObject other)
    {
        // Debug.Log($"{name} I am hit by {other.gameObject.name}");
        // Enemy explosion tick play on awake
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        // add selDistruct script to hitVFX
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        
        hitPoints--; //decrease healthPoint
        // GetComponent<MeshRenderer>().material.color = Color.magenta;
        scoreBoard.IncreaseScore(scorePerHit); // increase score
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        // Quaternion.identity --> no rotation necessary
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
