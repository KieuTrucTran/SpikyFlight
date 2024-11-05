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

    // Start is called before the first frame update
    void Start()
    {
        birdScript = birdObject.GetComponent<Bird>();

        makeSpikeCollection();
        spawnSpikes();
    }

    // Update is called once per frame
    void Update()
    {
        determineNextSpikeCollection();
        determineNextSpikePattern();
    }

    void determineNextSpikeCollection()
    {
        birdScript.getScore();
        if(birdScript.score < 10)
        {
            currentSpikeCollection = spikeCollections[0];
        }
        else if(birdScript.score < 20)
        {
            currentSpikeCollection = spikeCollections[1];
        }
        else if(birdScript.score < 30)
        {
            currentSpikeCollection = spikeCollections[2];
        }
        else if(birdScript.score < 40)
        {
            currentSpikeCollection = spikeCollections[3];
        }
        else if(birdScript.score < 50)
        {
            currentSpikeCollection = spikeCollections[4];
        }
        else if(birdScript.score < 60)
        {
            currentSpikeCollection = spikeCollections[5];
        }
        else if(birdScript.score < 70)
        {
            currentSpikeCollection = spikeCollections[6];
        }
        else if(birdScript.score < 80)
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
        // Instantiate left spikes
        /*for (int i = 0; i < 5; i++)
        {
            GameObject spike = Instantiate(spikePrefab);
            spike.transform.position = new Vector3(-2.815f, Random.Range(-3.5f, 3.5f), 0);
        }
        */
    }
}
