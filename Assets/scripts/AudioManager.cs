using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioController;

    private AudioSource _as;
    public AudioClip PlayerDeath;
    public AudioClip loseLife;
    public AudioClip collect;
    public AudioClip collectCoin;
    public AudioClip enemyDie;
    public AudioClip jump;
    public AudioClip pipeOut;
    public AudioClip platformFall;
    public AudioClip startLevel;
    public AudioClip wave;


    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        if (AudioController == null)
        {
            AudioController = this;
        }
        else
        {
            Debug.LogError("Too many Audio Controllers");
        }
    }

    public void PlayCommand(AudioClip sound)
    {
        _as.PlayOneShot(sound);
    }
    
}
