using App.Controllers;
using App.GameEvents;
using DynamicBox.EventManagement;
using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Traits;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Managers
{
  public class WaveManager : MonoBehaviour
  {
    [Header ("Parameters")]
    [Range (3, 10)][SerializeField] private int timeBetweenWaves = 5;
    [SerializeField] private int enemyPoolSize = 10;
    [SerializeField] private const int enemySpawnDelay = 500;

    [Header ("Links")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    private List<GameObject> enemyPool;
    private int waveNumber = 1;
    private GameObject enemyPoolParent;
    private const int enemyPoolSpawnDelay = 500;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<CheckForAliveEnemiesEvent> (CheckForAliveEnemiesEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<CheckForAliveEnemiesEvent> (CheckForAliveEnemiesEventHandler);
    }

    void Awake ()
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
        GameObject instanceObj = ObjectPool.Instantiate (enemyPrefab, enemyPoolParent.transform);
        await Task.Delay (enemyPoolSpawnDelay);
        enemyPool.Add (instanceObj);
      }

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
      GameObject obj = enemyPool.Find (o => !o.activeInHierarchy);
      if (obj == null)
      {
        // If no active enemies are available, create a new one
        obj = ObjectPool.Instantiate (enemyPrefab, enemyPoolParent.transform);
        enemyPool.Add (obj);
      }

      obj.SetActive (true);
      obj.GetComponent<EnemyController> ().ActivateEnemy (player);
    }

    private async void CheckForAliveEnemiesAsync ()
    {
      if (IsAnyEnemyAlive ())
        return;

      await Task.Delay (timeBetweenWaves);

      StartWaveAsync ();
    }

    private bool IsAnyEnemyAlive ()
    {
      GameObject firstAliveEnemy = enemyPool.Find (i => i.activeInHierarchy);

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
