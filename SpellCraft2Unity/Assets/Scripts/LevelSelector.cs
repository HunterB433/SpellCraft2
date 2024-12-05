using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public string rat_type;

    public Button[] buttons;

    // Start is called before the first frame update

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        Debug.Log($"ReachedIndex: {PlayerPrefs.GetInt("ReachedIndex")}, UnlockedLevel: {PlayerPrefs.GetInt("UnlockedLevel")}");


        //first set all the buttons to un-interactable
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        //next set all the buttons that can be interacted with to be interactable
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
    void Start()
    {
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(rat_type + " Rat");
    }
}
