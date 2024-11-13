using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource jump;
    public AudioSource collectBonbon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump.Play();
            // audioSource.clip = null;
        }
    }

    public void PlayBonbonSound()
    {
        collectBonbon.Play();
    }
}
