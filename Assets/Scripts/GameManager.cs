using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("GameObjects")]
    public GameObject ballAnchor1;
    public GameObject ballAnchor2;
    public Respawn resP1;
    public Respawn resP2;
    public bool RestartGame=false;
    public bool ShowUI;
    public int GameAudio;
    
    public int currentNumOfFlies = 0;
    public HashSet<int> TakenPlaces;
    
    public bool isGameActive;
    
    
    private bool canMoveP1 = true;
    private bool canMoveP2 = true;
    private int rightFrogScore;
    private int leftFrogScore;
    
    private float[] health;
    private float maxHealth;
    [SerializeField] private AudioSource SuperFlyAudio;


    
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        TakenPlaces = new System.Collections.Generic.HashSet<int>();
        rightFrogScore = 0;
        leftFrogScore = 0;
    }

    public void SetCanMove(bool value, String player)
    {
        if(player == "Player1")
        {
            canMoveP1 = value;
        }
        else if(player == "Player2")
        {
            canMoveP2 = value;
        }
    }

    public bool GetCanMove(String player)
    {
        if(player == "Player1")
        {
            return canMoveP1;
        }
        else 
        {
            return canMoveP2;
        }
    }

    public void AddScore(String player, int score)
    { 
        if(score>1)
        {
            SuperFlyAudio.Play();
        }
        if (player == "Player1")
        {
            leftFrogScore += score;
        }
        else
        {
            rightFrogScore += score;
        }
        if (leftFrogScore < 0)
        {
            leftFrogScore = 0;
        }
        if (rightFrogScore < 0)
        {
            rightFrogScore = 0;
        }
    }
    

    public int GetScore(String player)
    {
        if (player == "Player1")
        {
            return leftFrogScore;
        }
        else
        {
            return rightFrogScore;
        }
    }

    public void ResetPlayer(String player)
    {
        switch (player)
        {
            case "Player1":
                resP1.ResetLocation();
                break;
            case "Player2":
                resP2.ResetLocation();
                break;
        }
    }

    public void ChangeRestart()
    {
        RestartGame = true;
    }

    public bool CanRestart()
    {
        return RestartGame;
    }

    public void ChangeShowUI()
    {
        ShowUI = true;
    }

    public void ChangeGameAudio()
    {
        GameAudio+=1;
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        isGameActive = true;
    }
}
