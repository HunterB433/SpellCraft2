// Step 1: Create a Popup UI in Unity
// 1. In Unity, create a Canvas.
// 2. Add a Panel to the Canvas that will act as the background for the popup.
// 3. Inside the Panel, create an Image on the left for the person image and a Text (or TextMeshPro) element on the right for the dialogue.
// 4. Add a Button at the bottom for advancing the dialogue.

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class PopupSystem : MonoBehaviour
{
    public Image personImage;
    public Text dialogueText;
    public Button nextButton;

    private Queue<string> dialogueQueue;
    private Dictionary<string, Sprite> imageDictionary;

    void Start()
    {
        // Initialize the queue and dictionary
        dialogueQueue = new Queue<string>();
        imageDictionary = new Dictionary<string, Sprite>();

        // Load config data (you'll need to replace this with actual file loading)
        LoadConfigData();

        // Set up button listener
        nextButton.onClick.AddListener(ShowNextDialogue);
    }

    private void LoadConfigData()
    {
        // Replace with actual path or file reading
        string configPath = "C:\\Users\\hunte\\Desktop\\SpellCraft2\\SpellCraft2Unity\\Assets\\Scripts\\config.txt";
        string[] lines = File.ReadAllLines(configPath);

        foreach (string line in lines)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                string key = parts[1].Trim();
                string value = parts[0].Trim();

                if (key.StartsWith("IMAGE"))
                {
                    // Load image as a Sprite
                    Sprite image = LoadSpriteFromPath(value);
                    imageDictionary[key] = image;
                }
                else if (key.StartsWith("TEXT"))
                {
                    // Add text to the dialogue queue
                    dialogueQueue.Enqueue(value);
                }
            }
        }
    }

    private Sprite LoadSpriteFromPath(string path)
    {
        Texture2D texture = new Texture2D(2, 2);
        byte[] imageData = File.ReadAllBytes(path);
        texture.LoadImage(imageData);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public void ShowPopup(string imageKey)
    {
        if (imageDictionary.ContainsKey(imageKey))
        {
            personImage.sprite = imageDictionary[imageKey];
        }
        ShowNextDialogue();
    }

    private void ShowNextDialogue()
    {
        if (dialogueQueue.Count > 0)
        {
            dialogueText.text = dialogueQueue.Dequeue();
            nextButton.GetComponentInChildren<Text>().text = dialogueQueue.Count > 0 ? "Next" : "Done";
        }
        else
        {
            // Hide the popup when done
            gameObject.SetActive(false);
        }
    }
}
