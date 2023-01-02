using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
  [Header ("Parameters")]
  [Range (3f, 10f)] [SerializeField] private float timeBetweenWaves = 5f;
  [SerializeField] private int enemyPoolSize = 20;
  [SerializeField] private float countdown = 2f;
  [SerializeField] private const int enemySpawnDelay = 500;

  [Header ("Links")]
  [SerializeField] private GameObject enemyPrefab;
  [SerializeField] private Transform player;
  [SerializeField] private Transform spawnPoint;

  private List<GameObject> enemyPool;
  private int waveNumber = 1;
  private GameObject enemyPoolParent;

  void Start ()
  {
    enemyPool = new List<GameObject> ();

    enemyPoolParent = Instantiate (new GameObject ());
    enemyPoolParent.name = "Enemy Pool";

    for (int i = 0; i < enemyPoolSize; i++)
    {
      GameObject instanceObj = Instantiate (enemyPrefab, enemyPoolParent.transform);
      instanceObj.SetActive (false);
      enemyPool.Add (instanceObj);
    }
  }

  void Update ()
  {
    if (countdown <= 0f)
    {
      StartWaveAsync ();

      countdown = timeBetweenWaves;
    }

    countdown -= Time.deltaTime;
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
  }
}
