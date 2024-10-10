using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Make sure to include this for Button

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;

    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                // Attempt to find an existing instance
                _instance = FindObjectOfType<SceneController>();

                // If not found, create a new GameObject with SceneController
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SceneController");
                    _instance = obj.AddComponent<SceneController>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Ensure that there is only one instance
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persists the object across scenes
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // New function to load a scene based on the button clicked
    public void LoadSceneByButton(Button button)
    {
        // Get the button's name and load the corresponding scene
        string sceneName = button.name;
        LoadScene(sceneName);
    }
}
