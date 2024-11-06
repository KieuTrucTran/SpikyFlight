using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject spikeCollectionsObject;
    private GameObject currentSpikeCollection;
    private GameObject[] spikeCollections;

    public GameObject birdObject;
    private Bird birdScript;

    private GameObject currentSpikePattern;
    private GameObject lastSpikePattern;

    private bool lastFrameHitRightwall = false;

    // Start is called before the first frame update
    void Start()
    {
        birdScript = birdObject.GetComponent<Bird>();

        makeSpikeCollection();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(birdScript.hitRightwall != lastFrameHitRightwall)
        {
            determineNextSpikeCollection();
            determineNextSpikePattern();
            spawnSpikes();
        }
        lastFrameHitRightwall = birdScript.hitRightwall;
    }

    void determineNextSpikeCollection()
    {
        birdScript.getScore();
        if(birdScript.score < 5)
        {
            currentSpikeCollection = spikeCollections[0];
        }
        else if(birdScript.score < 10)
        {
            currentSpikeCollection = spikeCollections[1];
        }
        else if(birdScript.score < 15)
        {
            currentSpikeCollection = spikeCollections[2];
        }
        else if(birdScript.score < 20)
        {
            currentSpikeCollection = spikeCollections[3];
        }
        else if(birdScript.score < 25)
        {
            currentSpikeCollection = spikeCollections[4];
        }
        else if(birdScript.score < 30)
        {
            currentSpikeCollection = spikeCollections[5];
        }
        else if(birdScript.score < 35)
        {
            currentSpikeCollection = spikeCollections[6];
        }
        else if(birdScript.score < 40)
        {
            currentSpikeCollection = spikeCollections[7];
        }
    }

    void makeSpikeCollection()
    {
        spikeCollections = new GameObject[8];
        for (int i = 0; i < 8; ++i)
        {
            spikeCollections[i] = spikeCollectionsObject.transform.GetChild(i).gameObject;
        }
    }

    void determineNextSpikePattern()
    {
        // Randomly select a spike pattern from the current spike collection
        int randomIndex = Random.Range(0, currentSpikeCollection.transform.childCount);
        currentSpikePattern = currentSpikeCollection.transform.GetChild(randomIndex).gameObject;
    }

    void spawnSpikes()
    {
        if (birdScript.hitRightwall)
        {
            // Instantiate left spikes
            if (lastSpikePattern != null)
            {
                lastSpikePattern.SetActive(false);
            }
            lastSpikePattern = Instantiate(currentSpikePattern, new Vector3(-2.815f, 0, 0), Quaternion.identity);
            lastSpikePattern.SetActive(true);
        }
        else
        {
            // Instantiate right spikes
            if (lastSpikePattern != null)
            {
                lastSpikePattern.SetActive(false);
            }
            lastSpikePattern = Instantiate(currentSpikePattern, new Vector3(2.815f, 0, 0), Quaternion.identity);
            lastSpikePattern.SetActive(true);
            
        }
    }
}
