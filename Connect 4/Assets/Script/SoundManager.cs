using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip dropsound;
    public AudioClip gameoversound;
    public static SoundManager SM;
    private void Awake()
    {
        SM = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundClipPlay()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(dropsound);
    }
    public void GameoverSoundClip()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(gameoversound);
    }
}
