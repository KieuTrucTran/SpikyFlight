using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    public GameObject spikePrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnSpikes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnSpikes()
    {
        // Instantiate left spikes
        for (int i = 0; i < 5; i++)
        {
            GameObject spike = Instantiate(spikePrefab);
            spike.transform.position = new Vector3(-2.815f, Random.Range(-3.5f, 3.5f), 0);
        }
    }
}
