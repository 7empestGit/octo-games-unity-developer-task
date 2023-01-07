using App.Controllers;
using App.GameEvents;
using DynamicBox.EventManagement;
using Opsive.Shared.Game;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Managers
{
  public class WaveManager : MonoBehaviour
  {
    [Header ("Parameters")]
    [SerializeField] private int enemyPoolSize = 10;

    [Header ("Links")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform player;

    private List<GameObject> enemyPool;
    private GameObject enemyPoolParent;
    private const int enemyPoolSpawnDelay = 500;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<EnemyIsDeadEvent> (EnemyIsKilledEventHandler);
      EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<EnemyIsDeadEvent> (EnemyIsKilledEventHandler);
      EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
    }

    #endregion

    private void StartSpawning ()
    {
      enemyPool = new List<GameObject> ();

      enemyPoolParent = Instantiate (new GameObject ());
      enemyPoolParent.name = "Enemy Pool";

      SpawnEnemyPool ();
    }

    private async void SpawnEnemyPool ()
    {
      for (int i = 0; i < enemyPoolSize; i++)
      {
        int randomEnemyTypeIndex = Random.Range (0, enemyPrefabs.Count);
        GameObject instanceObj = ObjectPool.Instantiate (enemyPrefabs[randomEnemyTypeIndex], enemyPoolParent.transform);
        instanceObj.SetActive (false);
        enemyPool.Add (instanceObj);

        // activate first enemy without delay
        if (i == 0)
          ActivateFirstInactiveEnemy ();

        await Task.Delay (enemyPoolSpawnDelay);
      }
    }

    private void ActivateFirstInactiveEnemy ()
    {
      GameObject obj = enemyPool.Find (o => !o.activeInHierarchy);

      if (obj == null)
        return;

      obj.SetActive (true);
      obj.GetComponent<EnemyController> ().ActivateEnemy (player);
    }

    #region Event Handlers

    private void EnemyIsKilledEventHandler (EnemyIsDeadEvent eventDetails)
    {
      ActivateFirstInactiveEnemy ();
    }

    private void StartGameEventHandler (StartGameEvent eventDetails)
    {
      StartSpawning ();
    }

    #endregion
  }
}
