using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        // How many things are in an array
        int numbMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;

        if (numbMusicPlayer > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            // When reload the game, music will be continue
            DontDestroyOnLoad(gameObject);
        }
    }
    // Singleton pattern
}
