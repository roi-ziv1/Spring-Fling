using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (transform.position.y <= 0)
        {
            ResetLocation();
        }
    }


    public void ResetLocation()
    {
        GameManager.instance.AddScore(gameObject.name, -3);
        transform.position = startPos;
    }
}