using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : MonoBehaviour
{

    [SerializeField] private AudioClip sound;
    [SerializeField] private float volume = 1.0f;
    private AudioSource onClickSourceSound;
    
    // Start is called before the first frame update
    void Start()
    {
        onClickSourceSound = new AudioSource();
        onClickSourceSound = gameObject.AddComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void StartScene(string sceneName)
    {
        PlaySound();
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        PlaySound();
        Application.Quit();
    }
    private void PlaySound()
    {
        onClickSourceSound.clip = sound;
        onClickSourceSound.loop = false;
        onClickSourceSound.volume = volume;
        onClickSourceSound.Play();
    }
}

