using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance;

    void Awake()
    {
        // Ensure only one instance of this GameObject exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Prevent this GameObject from being destroyed between scenes
        DontDestroyOnLoad(gameObject);
    }
}
