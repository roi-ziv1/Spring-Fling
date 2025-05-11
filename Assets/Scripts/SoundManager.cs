using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource StartGameAudio;
    [SerializeField] private AudioSource InGameAudio;
    [SerializeField] private AudioSource EndGameAudio;
    private bool Isplaying;

    void Update()
    {
        if(GameManager.instance.GameAudio==1 && !Isplaying )
        {
            print("InGame");
            Isplaying = true;
            StartGameAudio.Stop();
            InGameAudio.Play();
        }
        if(GameManager.instance.GameAudio==2 && Isplaying)
        {
            print("endgameoud");
            InGameAudio.Stop();
            EndGameAudio.Play();
        }
    }
}
