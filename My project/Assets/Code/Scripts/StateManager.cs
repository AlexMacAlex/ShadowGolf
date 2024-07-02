using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
   public void LoadLevel (string levelName)
    {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) LoadLevel("Level2");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
