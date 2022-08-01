using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;

    ScoreBoard scoreBoard;

    void Start() 
    {
        // we have just one ScoreBoard
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // Player's laser collision on
    // OnParticleCollision is called when a particle hits a Collider.
    // This can be used to apply damage to a GameObject when hit by particles. --> laser
    void OnParticleCollision(GameObject other)
    {
        // Debug.Log($"{name} I am hit by {other.gameObject.name}");
        // Enemy explosion tick play on awake
        ProcessHit();
        KillEnemy();
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void KillEnemy()
    {
        // Quaternion.identity --> no rotation necessary
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    
}
