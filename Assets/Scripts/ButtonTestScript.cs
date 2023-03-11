using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTestScript : MonoBehaviour
{
    //input the string in unity
    public string newGameScene;

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
    public void StartGame()
    {
        Debug.Log(newGameScene);
        SceneManager.LoadScene(newGameScene);
    }
}
