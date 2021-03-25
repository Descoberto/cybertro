using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Gif Class
public class gif : MonoBehaviour
{
    // Attributes 
    public Texture2D[] frames;
    public short fps = 10;
    public bool pause = false;

    // Update Background
    void Update()
    {
        int index = (short)(Time.time * fps) % frames.Length;
        
        GetComponent<RawImage>().texture = frames[index];
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            pausa();
        }
    }
    
    // Pause
    public void pausa()
    {
        if (pause)
        {
            fps = 10;
            pause = false;
        }
        else
        {
            fps = 0;
            pause = true;
        }
    }
}
