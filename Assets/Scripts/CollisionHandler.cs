using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    // void OnCollisionEnter(Collision other) 
    // {
    //     Debug.Log(this.name + "--Collided with--" + other.gameObject.name);
    // }
    //or
    // Player's is triggered open 
    void OnTriggerEnter(Collider other) 
    {
        // Debug.Log($"{this.name} + **Triggered by** + {other.gameObject.name}");
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        crashVFX.Play();
        // when we crush, we disable the mesh renderer.
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        // Disable player controls
        GetComponent<PlayerControls>().enabled = false;
        // Wait 1 second
        Invoke("ReloadLevel", loadDelay);
        // Reload level
    }

    void ReloadLevel()
    {
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currenSceneIndex);
    }
}
