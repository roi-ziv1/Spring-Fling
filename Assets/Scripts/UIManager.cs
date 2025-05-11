using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rightScoreText;
    [SerializeField] private TextMeshProUGUI lefttScoreText;
    [SerializeField] private GameObject yafa;
    [SerializeField] private GameObject tirza;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image countDownBar;
    
    private float lerpTime;

    private float[] health;
    
    private float timeRemaining = 90f;

    public void Awake()
    {
        yafa.SetActive(false);
        tirza.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.ShowUI)
        {
            rightScoreText.text = "X " + GameManager.instance.GetScore("Player2");
            lefttScoreText.text = GameManager.instance.GetScore("Player1") + " X";
            if (GameManager.instance.GetScore("Player2") > 0)
            {
                rightScoreText.fontSize = 70;
            }

            if (GameManager.instance.GetScore("Player1") > 0)
            {
                lefttScoreText.fontSize = 70;
            }
        
            if (timeRemaining > 0 && GameManager.instance.isGameActive)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else if (timeRemaining <= 0)
            {
                print("yay");
                EndGame();
            }

            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }

    }

    private void EndGame()
    {
        GameManager.instance.ChangeGameAudio();
        if (GameManager.instance.GetScore("Player1") > GameManager.instance.GetScore("Player2"))
        {
            yafa.SetActive(true);
        }
        else
        {
            print("test");
            tirza.SetActive(true);
        }
        timerText.gameObject.SetActive(false);
        countDownBar.gameObject.SetActive(false);
        GameManager.instance.ChangeRestart();
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        float clamp = Mathf.Clamp01(timeToDisplay / 90);
        countDownBar.fillAmount = clamp;
    }
}
