using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSong : MonoBehaviour
{
    private AudioController music;

    public void Next(){
        music.Skip();
    }
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioController>();
    }

}
