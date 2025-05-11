using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTextDis : MonoBehaviour
{
    private RawImage rawImage;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        rawImage.enabled = false;
    }
    
    void Update()
    {
        if (GameManager.instance.ShowUI)
        {
            rawImage.enabled = true;
            Invoke(nameof(Disappear), 5f);
        }
    }
    
    void Disappear()
    {
        transform.gameObject.SetActive(false);
    }
}
