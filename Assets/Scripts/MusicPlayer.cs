using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Music Player
public class MusicPlayer : MonoBehaviour {

    // Attributes
    public AudioClip[] clips;
    private AudioSource mpSource;
    public bool pause = false;

    // Initialization
    void Start ()
    {
    	mpSource = FindObjectOfType<AudioSource>();
    }

    // Update 
    void Update ()
    {
	if (!mpSource.isPlaying)
        {
            mpSource.clip = GetRandomClip();
		
            if (pause == true)
            {
                mpSource.Pause();
            } else {
                mpSource.Play();
            }
        }
	    
        if (Input.GetKeyDown(KeyCode.P))
        {
            pausa();
        }
    }

    // Get Random Clip
    private AudioClip GetRandomClip()
    {
	// Execute Random Music
        return clips[Random.Range(0, clips.Length)]; 
    }

    //	Pause
    public void pausa()
    {
        if (pause)
        {
            pause = false;
        } else {
            pause = true;
		
            mpSource.Pause();
        }
    }
}
