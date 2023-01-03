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
    [SerializeField] private int enemyPoolSize = 10;

    [Header ("Links")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    private List<GameObject> enemyPool;
    private GameObject enemyPoolParent;
    private const int enemyPoolSpawnDelay = 500;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<EnemyIsKilledEvent> (EnemyIsKilledEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<EnemyIsKilledEvent> (EnemyIsKilledEventHandler);
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
      // spawn first one without delays
      GameObject instanceObj = ObjectPool.Instantiate (enemyPrefab, enemyPoolParent.transform);
      enemyPool.Add (instanceObj);
      ActivateFirstInactiveEnemy ();

      for (int i = 0; i < enemyPoolSize - 1; i++)
      {
        instanceObj = ObjectPool.Instantiate (enemyPrefab, enemyPoolParent.transform);
        await Task.Delay (enemyPoolSpawnDelay);
        enemyPool.Add (instanceObj);
      }
    }

    private void ActivateFirstInactiveEnemy ()
    {
      GameObject obj = enemyPool.Find (o => !o.activeInHierarchy);

      if (obj == null && !IsAnyEnemyAlive ())
        return;

      obj.SetActive (true);
      obj.GetComponent<EnemyController> ().ActivateEnemy (player);
    }

    private bool IsAnyEnemyAlive ()
    {
      GameObject firstActiveEnemy = enemyPool.Find (obj => obj.activeInHierarchy);

      return firstActiveEnemy != null;
    }

    #region Event Handlers

    private void EnemyIsKilledEventHandler (EnemyIsKilledEvent eventDetails)
    {
      ActivateFirstInactiveEnemy ();
    }

    #endregion
  }
}
