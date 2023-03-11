using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/*Fix � faire
 * Puller les files d'un dossier 
 * essayer de l'int�grer dans le controleur
 * pour l'instant c'Est les m�mes tuens et �a se d�clenche direct
 * 
 * 
 * 
 * 
 * 
 * 
 * Gonna kill myself
 * 
 */
public class Playlist : MonoBehaviour
{

    public AudioClip[] playlist;

    public AudioClip randomClip;

    public RandomAudioClip playlistRandom;

    public AudioSource currentSong;

    // Start is called before the first frame update
    void Start()
    {
        RandomAudioClip playlistRandom = new RandomAudioClip();
        currentSong = new AudioSource();
       
        Debug.Log(playlist);
        randomClip = playlist[Random.Range(0, playlist.Length-1)];

        currentSong = gameObject.AddComponent<AudioSource>();
        currentSong.clip = randomClip;
        currentSong.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentSong.isPlaying)
        {
            randomClip = playlist[Random.Range(0, playlist.Length - 1)];
            currentSong.Play();
            currentSong.loop = false;
        }
    }
}

[System.Serializable]
public class RandomAudioClip
{

    public AudioClip[] audioClipArray;

    private int audioClipIndex;
    private int[] previousArray;
    private int previousArrayIndex;

    // The best random method
    public AudioClip GetRandomAudioClip()
    {
        // Initialize
        if (previousArray == null)
        {
            // Sets the length to half of the number of AudioClips
            // This will round downwards
            // So it works with odd numbers like for example 3
            previousArray = new int[audioClipArray.Length / 2];
        }
        if (previousArray.Length == 0)
        {
            // If the the array length is 0 it returns null
            return null;
        }
        else
        {
            // Psuedo random remembering previous clips to avoid repetition
            do
            {
                audioClipIndex = Random.Range(0, audioClipArray.Length);
            } while (PreviousArrayContainsAudioClipIndex());
            // Adds the selected array index to the array
            previousArray[previousArrayIndex] = audioClipIndex;
            // Wrap the index
            previousArrayIndex++;
            if (previousArrayIndex >= previousArray.Length)
            {
                previousArrayIndex = 0;
            }
        }

        // Returns the randomly selected clip
        return audioClipArray[audioClipIndex];
    }

    // Returns if the randomIndex is in the array
    private bool PreviousArrayContainsAudioClipIndex()
    {
        for (int i = 0; i < previousArray.Length; i++)
        {
            if (previousArray[i] == audioClipIndex)
            {
                return true;
            }
        }
        return false;
    }
}