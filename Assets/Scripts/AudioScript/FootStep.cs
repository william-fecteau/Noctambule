using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    bool isPlayerOnButton;
    [SerializeField] private AudioClip[] playlist;
    [SerializeField] private AudioClip randomClip;
    [SerializeField] private AudioSource currentSong;
    [SerializeField] private float volume = 0.6f;
    GameObject player;
    void Start()
    {
        isPlayerOnButton = false;
        player = GameObject.FindGameObjectWithTag("Player");
        currentSong = new AudioSource();
        currentSong = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        //this.buttonPress(isPlayerOnButton);
        if (isPlayerOnButton)
        {
            if (!currentSong.isPlaying)
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

                randomClip = playlist[Random.Range(0, playlist.Length - 1)];
                currentSong.clip = randomClip;
                currentSong.loop = false;
                currentSong.volume = volume;
                currentSong.Play();
            }
        }
        if (!currentSong.isPlaying)
        {
            currentSong.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTER@@@@");
        Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
            Debug.Log("ENTER");
            isPlayerOnButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("EXIT");
            isPlayerOnButton = false;
        }
    }
}
