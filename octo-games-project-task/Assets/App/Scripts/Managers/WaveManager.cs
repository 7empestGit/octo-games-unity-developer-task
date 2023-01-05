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
    [SerializeField] private List<GameObject> enemyPrefabs;
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

    private void EnemyIsKilledEventHandler (EnemyIsKilledEvent eventDetails)
    {
      ActivateFirstInactiveEnemy ();
    }

    #endregion
  }
}
