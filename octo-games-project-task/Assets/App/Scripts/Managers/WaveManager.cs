using App.Controllers;
using App.GameEvents;
using DynamicBox.EventManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Managers
{
  public class WaveManager : MonoBehaviour
  {
    [Header ("Parameters")]
    [Range (3, 10)][SerializeField] private int timeBetweenWaves = 5;
    [SerializeField] private int enemyPoolSize = 20;
    [SerializeField] private const int enemySpawnDelay = 500;

    [Header ("Links")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;

    private List<GameObject> enemyPool;
    private int waveNumber = 1;
    private GameObject enemyPoolParent;

    private const int enemyPoolSpawnDelay = 0;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<CheckForAliveEnemiesEvent> (CheckForAliveEnemiesEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<CheckForAliveEnemiesEvent> (CheckForAliveEnemiesEventHandler);
    }

    void Start ()
    {
      enemyPool = new List<GameObject> ();

      enemyPoolParent = Instantiate (new GameObject ());
      enemyPoolParent.name = "Enemy Pool";

      SpawnEnemyPool ();
    }

    #endregion

    private async void SpawnEnemyPool ()
    {
      for (int i = 0; i < enemyPoolSize; i++)
      {
        GameObject instanceObj = Instantiate (enemyPrefab, enemyPoolParent.transform);
        await Task.Delay (enemyPoolSpawnDelay);
        instanceObj.SetActive (false);
        enemyPool.Add (instanceObj);
      }

      StartWaveAsync ();
    }

    private async void CheckForAliveEnemiesAsync ()
    {
      if (IsAnyEnemyAlive ())
        return;

      await Task.Delay (timeBetweenWaves);

      StartWaveAsync ();
    }

    private async void StartWaveAsync ()
    {
      int enemyCount = waveNumber;

      for (int i = 0; i < enemyCount; i++)
      {
        SpawnEnemy ();

        await Task.Delay (enemySpawnDelay);
      }

      waveNumber++;
    }

    private void SpawnEnemy ()
    {
      GameObject obj = enemyPool.Find (o => !o.gameObject.activeInHierarchy);
      if (obj == null)
      {
        // If no inactive objects are available, create a new one
        obj = Instantiate (enemyPrefab, enemyPoolParent.transform);
        enemyPool.Add (obj);
      }

      obj.SetActive (true);
      obj.transform.position = spawnPoint.position;
      obj.transform.rotation = spawnPoint.rotation;

      obj.GetComponent<EnemyController> ().SetTarget (player);
      obj.GetComponent<EnemyController> ().SetDestinationAsync ();
    }


    private bool IsAnyEnemyAlive ()
    {
      GameObject firstAliveEnemy = enemyPool.Find (i => i.gameObject.activeSelf == true);

      return firstAliveEnemy != null;
    }

    #region Event Handlers

    private void CheckForAliveEnemiesEventHandler (CheckForAliveEnemiesEvent eventDetails)
    {
      CheckForAliveEnemiesAsync ();
    }

    #endregion
  }
}
