using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;
    
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private int _numberOfWaves = 5;
    [SerializeField] private float _spawnInterval = 2.0f;
    [SerializeField] private float _waveDelay = 2.0f;
    
    [SerializeField] CameraTrigger _cameraTrigger;
    
    private List<GameObject> _enemyList = new List<GameObject>();
    
    private int _currentWave = 0;
    
    public UnityEvent FinalWaveCompleteEvent;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        TutorialManager.Instance.CombatTutorialCompleteEvent.AddListener(StartWave);
        PlayerStats.Instance.ScreenChangeEvent.AddListener(StartWave);
    }
	
    private void OnDisable()
    {
        TutorialManager.Instance.CombatTutorialCompleteEvent.RemoveListener(StartWave);
        PlayerStats.Instance.ScreenChangeEvent.RemoveListener(StartWave);
    }
    
    private IEnumerator SpawnEnemies(int enemiesPerWave)
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(0.5f, _spawnInterval));
        }
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(_waveDelay);
        
        if (PlayerStats.Instance.IsShopping == false)
        {
            if (_currentWave < _numberOfWaves)
            {
                int enemiesPerWave = GetEnemiesPerWave(_currentWave);

                _currentWave++;

                Debug.Log($"Starting Wave {_currentWave} with {enemiesPerWave} enemies.");

                StartCoroutine(SpawnEnemies(enemiesPerWave));
            }
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

        GameObject enemy = Instantiate(_enemyPrefabs[Random.Range(0,2)], spawnPosition, Quaternion.identity);
        _enemyList.Add(enemy);
        
    }


    // Method to determine the number of enemies per wave
    private int GetEnemiesPerWave(int waveIndex)
    {
        return 3 + waveIndex; // 3 enemies in the first wave, 4 in the second, etc.
        
        // Animation curve??
    }

    private void StartWave()
    {
        StartCoroutine(SpawnWave());
    }
    
    public void EnemyDowned(GameObject enemy)
    {
        _enemyList.Remove(enemy);
        
        if (_enemyList.Count == 0)
        {
            if (_currentWave == _numberOfWaves)
            {
                Debug.Log("Final wave is Complete!");
                FinalWaveCompleteEvent.Invoke();
            }
            else
            {
                Debug.Log("Wave complete! All enemies downed.");
                this.DelayAction(_cameraTrigger.OpenStore, 2.0f);
            }
        }
    }
}