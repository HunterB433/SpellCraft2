using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.LightingExplorerTableColumn;

public class Main_Menu_Loader : MonoBehaviour
{
    public void OpenScene()
    {
        //reset the playerPerfs
        PlayerPrefs.SetInt("ReachedIndex", 0);
        PlayerPrefs.SetInt("UnlockedLevel", 1);

    }
}
