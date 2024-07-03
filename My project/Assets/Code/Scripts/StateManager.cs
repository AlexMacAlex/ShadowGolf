using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager main;


    // Start is called before the first frame update
   public void LoadLevel (string levelName)
    {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }

    private void Awake()
    {
        main = this;   
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        CommunicationArduino.main.CloseDataStream();
        switch (currentLevel)
        {
            case "Level1":
                //SceneManager.UnloadSceneAsync(currentLevel);
                LoadLevel("Level2");
                break;

            case "Level2":
                //SceneManager.UnloadSceneAsync(currentLevel);
                LoadLevel("Level1");
                break;

        }
    }

    public void ReloadLevel()
    {
        CommunicationArduino.main.CloseDataStream();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
