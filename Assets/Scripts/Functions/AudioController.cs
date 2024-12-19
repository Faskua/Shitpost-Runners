using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioClip mondongo;
    public List<AudioClip> audios;
    private static AudioSource source;
    private int index = 0;

    public void Mondongo(){
        Stop();
        source.clip = mondongo;
        source.loop = false;
        source.Play();
    }

    public void Stop() => source.Stop();

    public void Skip(){
        source.clip = audios[index++];
        source.Play();
    }

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        Shuffle();
        index = 0;
        source.clip = audios[index++];
        source.Play();
    }

    void Update(){
        if(index == audios.Count){
            index = 0;
            Shuffle();
        }
        if(!source.isPlaying){
            Skip();
        }   
    }

    void Shuffle(){
        System.Random random = new System.Random();
        for (int i = 0; i < audios.Count; i++)
        {
            int j = random.Next(0, audios.Count);
            AudioClip assistent = audios[i];
            audios[i] = audios[j];
            audios[j] = assistent;
        }
    }
}
