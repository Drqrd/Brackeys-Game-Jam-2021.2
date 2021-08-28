using System;
using UnityEngine;


public class audioManager : MonoBehaviour
{
    public Sound[] sounds;



    private void Awake(){
        foreach(Sound sound in sounds){
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start(){
        this.playAudio("Theme");
    }



    public void playAudio(string clipName){
        Sound requestedSound = Array.Find(sounds, sound => sound.name == clipName);
        requestedSound.source.Play();
    }


}
