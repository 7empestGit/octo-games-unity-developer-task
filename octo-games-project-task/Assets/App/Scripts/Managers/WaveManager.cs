using App.Controllers;
using App.GameEvents;
using DynamicBox.EventManagement;
using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Game;
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
    [SerializeField] private SpawnPoint spawnPoint;
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
      EventManager.Instance.AddListener<PlayerIsDeadEvent> (PlayerIsDeadEventHandler);
      EventManager.Instance.AddListener<RestartGameEvent> (RestartGameEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<EnemyIsDeadEvent> (EnemyIsKilledEventHandler);
      EventManager.Instance.RemoveListener<StartGameEvent> (StartGameEventHandler);
      EventManager.Instance.RemoveListener<PlayerIsDeadEvent> (PlayerIsDeadEventHandler);
      EventManager.Instance.RemoveListener<RestartGameEvent> (RestartGameEventHandler);
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
          SpawnFirstInactiveEnemy ();

        await Task.Delay (enemyPoolSpawnDelay);
      }
    }

    private void SpawnFirstInactiveEnemy ()
    {
      GameObject obj = enemyPool.Find (o => !o.activeInHierarchy);

      if (obj == null)
        return;

      obj.SetActive (true);
      SetEnemyPlacement (obj);
      obj.GetComponent<EnemyController> ().ActivateEnemy (player);
    }

    private void SetEnemyPlacement (GameObject obj)
    {
      Vector3 randomPositon = Vector3.zero;
      Quaternion randomRotation = Quaternion.identity;
      if (spawnPoint.GetPlacement (obj, ref randomPositon, ref randomRotation))
      {
        obj.transform.position = randomPositon;
        obj.transform.rotation = randomRotation;
      }
    }

    #region Event Handlers

    private void EnemyIsKilledEventHandler (EnemyIsDeadEvent eventDetails)
    {
      SpawnFirstInactiveEnemy ();
    }

    private void StartGameEventHandler (StartGameEvent eventDetails)
    {
      StartSpawning ();
    }

    private void PlayerIsDeadEventHandler (PlayerIsDeadEvent eventDetails)
    {
      foreach (GameObject enemy in enemyPool)
      {
        enemy.GetComponent<EnemyController> ().DeactivateEnemy ();
      }
    }

    private void RestartGameEventHandler (RestartGameEvent eventDetails)
    {
      foreach (GameObject enemy in enemyPool)
      {
        enemy.GetComponent<EnemyController> ().DisableEnemy ();
      }
      SpawnFirstInactiveEnemy ();
    }

    #endregion
  }
}
