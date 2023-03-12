using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FootStep : MonoBehaviour
{
    bool isPlayerOnButton;
    [SerializeField] private AudioClip[] playlist;
    [SerializeField] private AudioClip randomClip;
    [SerializeField] private AudioSource currentSong;
    [SerializeField] private float volume = 1.0f;
    [SerializeField] private TilemapCollider2D tilemap;
    GameObject player;
    void Start()
    {
        isPlayerOnButton = false;
        player = GameObject.FindGameObjectWithTag("Player");
        currentSong = new AudioSource();
        currentSong = gameObject.AddComponent<AudioSource>();
        Debug.Log(tilemap);
        Debug.Log(tilemap.IsTouching(player.GetComponent<BoxCollider2D>()));

    }

    void Update()
    {
        if (isPlayerOnButton)
        {
            if (!currentSong.isPlaying)
            {
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    randomClip = playlist[Random.Range(0, playlist.Length - 1)];
                    currentSong.clip = randomClip;
                    currentSong.loop = false;
                    currentSong.volume = volume;
                    currentSong.Play();
                }
                
            }
        }
        if (!currentSong.isPlaying)
        {
            currentSong.Stop();
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
