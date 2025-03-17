using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // This static instance ensures only one AudioManager exists
    private static AudioManager instance;

    void Awake()
    {
        // If an instance doesn't exist, assign this one and persist it across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the AudioManager when loading new scenes
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
}
