using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    public AudioSource Source;

    public AudioClip Clip;
    
    // Start is called before the first frame update
    void Start()
    {
        Source.clip = Clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        Source.Play();
    }
}
