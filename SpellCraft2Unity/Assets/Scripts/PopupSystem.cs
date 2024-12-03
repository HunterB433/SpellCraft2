using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PopupSystem : MonoBehaviour
{
    public SceneController sceneController;
    public GameObject otherCanvas;

    public GameObject PopupPane;            // Panel for the PopUp (USE PREFAB)
    public Image tempImage;                 // The Image component in the panel
    public TextMeshProUGUI dialogueText;    // The TextMeshProUGUI DIALOGUE component in the panel
    public TextMeshProUGUI titleText;       // The TextMeshProUGUI TITLE component in the panel

    public Button nextButton;               // The Button to trigger the change in text
    public Button playGuideButton;          // Used to start the POPUP (Might try to remove this later b/c
                                            // There can be better ways
    public Button startButton;              // Used to start the game after this

    public string ToBeTitle;

    public bool InstaPlay;
    public bool InstaSkip;

    public List<Sprite> images = new List<Sprite>();                     // List to hold all images to display
    [SerializeField] private List<string> messages = new List<string>(); // List to hold multiple messages
    [SerializeField] private List<int> imageIndices = new List<int>();   // Array of image indices corresponding to the messages

    private int currentMessageIndex = 0;    // Index to keep track of current message

    void Start()
    {
        if (InstaPlay)
        { 
        PopupPane.SetActive(true);
        otherCanvas?.SetActive(false);
        }
        else
            PopupPane.SetActive(false);

        // Initially set the image and first message
        if (images.Count > 0 && messages.Count > 0)
        {
            tempImage.sprite = images[imageIndices[0]]; // Set the first image based on the index
            dialogueText.text = messages[0];
        }

        // These may be removed later
        // Add a listener to the button to trigger the change when clicked
        nextButton.onClick.AddListener(OnNextButtonClicked);
        // Add a listener to the play button to trigger scene
        playGuideButton.onClick.AddListener(OnPlayButtonClicked);

        titleText.text = ToBeTitle;
    }
    void OnPlayButtonClicked()
    {
        // When the Play button is clicked, enable the PopupPane
        PopupPane.SetActive(true);
        HideButton(playGuideButton);
        otherCanvas?.SetActive(false);
    }

    void OnNextButtonClicked()
    {
        // If we still have messages left in the list
        if (currentMessageIndex < messages.Count - 1)
        {
            // Go to next message
            currentMessageIndex++;
            dialogueText.text = messages[currentMessageIndex];

            // Set the image based on the image index for the current message
            if (currentMessageIndex < imageIndices.Count)
            {
                int imageIndex = imageIndices[currentMessageIndex];
                if (imageIndex >= 0 && imageIndex < images.Count)
                {
                    tempImage.sprite = images[imageIndex];
                }
                else
                {
                    Debug.LogWarning("Image index out of range.");
                }
            }
        }
        else
        {
            // Hide the Panel after showing all messages
            // Make the START button Show up
            if (!InstaSkip)
            {
                HidePanel();
                ShowButton(startButton);
                otherCanvas?.SetActive(true);
            }
            else
            {
                sceneController.LoadSceneByButton(startButton);
            }
        }
    }

    void HidePanel()
    {
        // Disable the entire panel object
        if (PopupPane != null)
        {
            PopupPane.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Panel is not assigned in the inspector.");
        }
    }

    void HideButton(Button button)
    {
        // Disable the entire panel object
        if (button != null)
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Button is not assigned in the inspector.");
        }
    }

    void ShowButton(Button button)
    {
        // Disable the entire panel object
        if (button != null)
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Button is not assigned in the inspector.");
        }
    }

}
