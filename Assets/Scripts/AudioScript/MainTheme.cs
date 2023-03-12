using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTheme : MonoBehaviour
{

    [SerializeField] private AudioClip mainClip;
    [SerializeField] private AudioSource currentSong;
    [SerializeField] private float volume;
    // Start is called before the first frame update
    void Start()
    {
        currentSong = new AudioSource();
        currentSong = gameObject.AddComponent<AudioSource>();
        currentSong.clip = mainClip;
        currentSong.loop = true;
        currentSong.volume = volume;
        currentSong.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
