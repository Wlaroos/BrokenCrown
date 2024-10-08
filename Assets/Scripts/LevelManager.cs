using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour

{
    public static LevelManager Instance;

    // Singleton
    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update() 
    {
        // Restart Level
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            MusicManager.Instance.SwapTrack(true);
        }
        
        // Exit Game
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
        
        // Reroll Shop
        if (Input.GetKeyDown(KeyCode.P)) 
        { 
            ShopPedestal[] tet = FindObjectsOfType<ShopPedestal>();
            
            foreach (var VARIABLE in tet)
            {
                VARIABLE.Reroll();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.O)) 
        { 
            PlayerStats.Instance.ChangeMoney(.05f);
        }
    }
}
