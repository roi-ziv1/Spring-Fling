using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject startImg;
    [SerializeField] private GameObject rulesImg;
    [SerializeField] private GameObject countDownBar;

    private int pressingCounter;

    private void Awake()
    {
        countDownBar.SetActive(false);
        startImg.SetActive(true);
        rulesImg.SetActive(false);
    }

    private void Start()
    {
        pressingCounter = 0;

    }

    public void OnClickStart(InputAction.CallbackContext callbackContext)
    {
        print("Check");
        if (callbackContext.started)  // first press to open rules page
        {
            if (pressingCounter > 0) // there was a press, so start the game
            {
                startImg.SetActive(false);
                rulesImg.SetActive(false);
                countDownBar.SetActive(true);
                StartCoroutine(GameManager.instance.StartGame());
                GameManager.instance.ChangeShowUI();
                if(GameManager.instance.GameAudio==0)
                {
                    GameManager.instance.ChangeGameAudio();
                }
            }
            else
            {
                rulesImg.SetActive(true);
                pressingCounter++;
            }
            if (GameManager.instance.CanRestart())
            {
                SceneManager.LoadScene(0);
            }
        }

    }
}
