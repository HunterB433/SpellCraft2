using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_health_display : MonoBehaviour
{
    public Text hitPointsText;

    [Header("Inscribed")]
    public int current_health = 10;
    public int starting_health = 10;

    // Start is called before the first frame update
    void Start()
    {
        hitPointsText.text = "HP: " + current_health.ToString() + '/' + starting_health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
