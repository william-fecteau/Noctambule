using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Button btn = yourButton.GetComponent<Button>();
        //btn.onClick.AddListener(TaskOnClick);
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void StartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
