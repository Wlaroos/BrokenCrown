using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() 
    {
        // Restart Level
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        // Exit Game
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }
}
