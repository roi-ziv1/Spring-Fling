using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed = 10;
    public Animator anim;
    private bool LT;
    private bool RT;
    
    void Start()
    {
        if(gameObject.name=="Player1")
        {
            rotateSpeed=-rotateSpeed;
        }
    }
    public void RotateLeft(InputAction.CallbackContext callbackContext)
    {
        if (GameManager.instance.ShowUI == false)
        {
            return;
        }
        LT = !callbackContext.canceled;

    }
    public void RotateRight(InputAction.CallbackContext callbackContext)
    {
        if(GameManager.instance.ShowUI==false)
        {
            return;
        }
        RT = !callbackContext.canceled;

    }

    private void FixedUpdate()
    {
        if (GameManager.instance.GetCanMove(gameObject.name))
        {
            if (LT)
            {
                transform.Rotate(Vector3.up, gameObject.name == "Player1" ?  rotateSpeed: -rotateSpeed);
                anim.SetFloat("Blend", 1 - (this.transform.rotation.eulerAngles.y / 360));

            }
            else if (RT)
            {
                transform.Rotate(Vector3.up, gameObject.name == "Player1" ? -rotateSpeed: rotateSpeed);
                anim.SetFloat("Blend", 1 - (this.transform.rotation.eulerAngles.y / 360));
            }
        }
    }
}
