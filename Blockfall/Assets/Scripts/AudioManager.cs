using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance = null;

    public AudioClip rotateSound;
    public AudioClip rowDelete;
    public AudioClip shapeMove;
    public AudioClip gameOver;


    private AudioSource soundEffectAudio;

   
    void Start() {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        AudioSource source = GetComponent<AudioSource>();

        soundEffectAudio = source;


    }

    public void PlayOneShot(AudioClip clip){
        soundEffectAudio.PlayOneShot(clip);
    }
}
