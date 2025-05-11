using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    private Vector3 _direction;
    public float scaleJump = 0.8f;
    private Rigidbody rb;

    [SerializeField] private float jumpForce = 5f;  // Increased jump force for quicker jump
  // Increased jump force for quicker jump
    private bool isGrounded = true;
    [SerializeField] private float gravity = 30f;
    [SerializeField] private GameObject playerSprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
        playerSprite.transform.position = transform.position;
    }

    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (GameManager.instance.isGameActive && GameManager.instance.GetCanMove(gameObject.name) && isGrounded && callbackContext.started)
        {
            rb.AddForce(-transform.right * jumpForce * scaleJump, ForceMode.VelocityChange);  // Use VelocityChange for instant speed change
            rb.velocity += Vector3.up * jumpForce;
            print(gameObject.name);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}