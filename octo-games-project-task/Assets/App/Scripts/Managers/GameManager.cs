using App.GameEvents;
using App.GameEvents.UI;
using DynamicBox.EventManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  private int currentKilledEnemies;
  private int killedEnemiesRecord;

  #region Unity Methods

  void OnEnable ()
  {
    EventManager.Instance.AddListener<EnemyIsDeadEvent> (EnemyIsDeadEventHandler);
    EventManager.Instance.AddListener<RestartGameEvent> (RestartGameEventHandler);
    EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
  }

  void Awake ()
  {
    // PlayerPrefs cases
    PlayerPrefs.SetInt ("CurrentKilledEnemies", currentKilledEnemies);

    if (!PlayerPrefs.HasKey ("KilledEnemiesRecord"))
    {
      PlayerPrefs.SetInt ("KilledEnemiesRecord", 0);
    }
  }

  void OnDisable ()
  {
    EventManager.Instance.RemoveListener<EnemyIsDeadEvent> (EnemyIsDeadEventHandler);
    EventManager.Instance.RemoveListener<RestartGameEvent> (RestartGameEventHandler);
    EventManager.Instance.RemoveListener<StartGameEvent> (StartGameEventHandler);
  }

  #endregion


  #region Event Handlers

  private void EnemyIsDeadEventHandler (EnemyIsDeadEvent eventDetails)
  {
    // increase CurrentKilledEnemies by 1 and save it
    currentKilledEnemies = PlayerPrefs.GetInt ("CurrentKilledEnemies");
    PlayerPrefs.SetInt ("CurrentKilledEnemies", ++currentKilledEnemies);

    EventManager.Instance.Raise (new UpdateHUDEvent (currentKilledEnemies));

    killedEnemiesRecord = PlayerPrefs.GetInt ("KilledEnemiesRecord");
    if (currentKilledEnemies >= killedEnemiesRecord)
      PlayerPrefs.SetInt ("KilledEnemiesRecord", currentKilledEnemies);
  }

  private void StartGameEventHandler (StartGameEvent eventDetails)
  {
    ResetCurrentKilledEnemies ();
    EventManager.Instance.Raise (new UpdateHUDEvent (currentKilledEnemies));
  }

  private void RestartGameEventHandler (RestartGameEvent eventDetails)
  {
    ResetCurrentKilledEnemies ();
    EventManager.Instance.Raise (new UpdateHUDEvent (currentKilledEnemies));
  }

  #endregion

  private void ResetCurrentKilledEnemies ()
  {
    currentKilledEnemies = 0;
    PlayerPrefs.SetInt ("CurrentKilledEnemies", currentKilledEnemies);
  }
}
