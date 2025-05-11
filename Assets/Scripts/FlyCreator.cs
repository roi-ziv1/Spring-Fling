using System.Collections;
using UnityEngine;

public class FlyCreator : MonoBehaviour
{
    [SerializeField] private Transform[] flyGeneratePlaces;
    [SerializeField] private GameObject[] flies;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentNumOfFlies < 4)
        {
            GenerateFly();
        }
    }

    private void GenerateFly()
    {
        int randPlace = Random.Range(0, 19);
        while (GameManager.instance.TakenPlaces.Contains(randPlace))
        {
            randPlace = Random.Range(0, 19);
        }

        GameManager.instance.TakenPlaces.Add(randPlace);
        GameObject go = PickFly();
        Instantiate(go, flyGeneratePlaces[randPlace]);
        GameManager.instance.currentNumOfFlies++;
    }

    private GameObject PickFly()
    {
        float rand = Random.Range(0f, 1f);

        if (rand < 0.7f)
        {
            return flies[0];
        }
        else if (rand < 0.9f)
        {
            return flies[1];
        }
        else
        {
            return flies[2];
        }
    }
}
