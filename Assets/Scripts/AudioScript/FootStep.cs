using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    bool isPlayerOnButton;
    [SerializeField] private AudioClip[] playlist;
    [SerializeField] private AudioClip randomClip;
    [SerializeField] private AudioSource currentSong;

    void Start()
    {
        isPlayerOnButton = false;
    }

    void Update()
    {
        if (isPlayerOnButton)
        {
            currentSong = new AudioSource();

            randomClip = playlist[Random.Range(0, playlist.Length - 1)];

            currentSong = gameObject.AddComponent<AudioSource>();
            currentSong.clip = randomClip;
            currentSong.loop = false;
            currentSong.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerOnButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerOnButton = false;
        }
    }
}
