using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _numberOfWaves = 5;
    [SerializeField] private float _spawnInterval = 2.0f;
    
    private int _currentWave = 0;

    private void Start()
    {
        TutorialManager.Instance.CombatTutorialCompleteEvent.AddListener(SpawnWave);
        PlayerStats.Instance.ScreenChangeEvent.AddListener(SpawnWave);
    }
	
    private void OnDisable()
    {
        TutorialManager.Instance.CombatTutorialCompleteEvent.RemoveListener(SpawnWave);
        PlayerStats.Instance.ScreenChangeEvent.RemoveListener(SpawnWave);
    }
    
    private IEnumerator SpawnEnemies(int enemiesPerWave)
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(0.5f, _spawnInterval));
        }
    }

    private void SpawnEnemy()
    {
        // Get the camera's viewport
        Camera camera = Camera.main;

        // Define spawn position outside of screen bounds
        Vector3 spawnPosition;

        // Randomly choose a side to spawn from
        int spawnSide = Random.Range(0, 4); // 0: left, 1: right, 2: top, 3: bottom

        switch (spawnSide)
        {
            case 0: // Left
                spawnPosition = new Vector3(-camera.orthographicSize * camera.aspect - 1f, Random.Range(-camera.orthographicSize, camera.orthographicSize), 0);
                break;
            case 1: // Right
                spawnPosition = new Vector3(camera.orthographicSize * camera.aspect + 1f, Random.Range(-camera.orthographicSize, camera.orthographicSize), 0);
                break;
            case 2: // Top
                spawnPosition = new Vector3(Random.Range(-camera.orthographicSize * camera.aspect, camera.orthographicSize * camera.aspect), camera.orthographicSize + 1f, 0);
                break;
            case 3: // Bottom
                spawnPosition = new Vector3(Random.Range(-camera.orthographicSize * camera.aspect, camera.orthographicSize * camera.aspect), -camera.orthographicSize - 1f, 0);
                break;
            default:
                spawnPosition = Vector3.zero; // Fallback in case of an error
                break;
        }

        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }


    // Method to determine the number of enemies per wave
    private int GetEnemiesPerWave(int waveIndex)
    {
        return 3 + waveIndex; // 3 enemies in the first wave, 4 in the second, etc.
        
        // Animation curve??
    }

    private void SpawnWave()
    {
        if (PlayerStats.Instance.IsShopping != false) return;
        if (_currentWave < _numberOfWaves)
        {
            int enemiesPerWave = GetEnemiesPerWave(_currentWave);
            
            _currentWave++;
            
            Debug.Log($"Starting Wave {_currentWave} with {enemiesPerWave} enemies.");
            
            StartCoroutine(SpawnEnemies(enemiesPerWave));
        }
        else
        {
            Debug.Log("All waves have been spawned!");
        }
    }
}