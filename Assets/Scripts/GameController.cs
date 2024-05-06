using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text scoreLabel;
    public static int score = 0;
    public GameObject menu;
    public UnityEngine.UI.Button startButton;
    public static bool isStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(delegate
        {
            menu.SetActive(false);
            isStarted = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + score;
    }
}
