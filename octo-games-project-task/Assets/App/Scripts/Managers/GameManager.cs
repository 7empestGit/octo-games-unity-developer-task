using UnityEngine;
using DynamicBox.EventManagement;
using App.GameEvents;

public class GameManager : MonoBehaviour
{
  [Header ("Links")]
  [SerializeField] private GameObject character;


  #region Unity Methods

  void OnEnable ()
  {
    EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
  }

  void OnDisable ()
  {
    EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
  }

  #endregion

  #region Event Handlers

  private void StartGameEventHandler (StartGameEvent eventDetails)
  {
    character.SetActive (true);
  }

  #endregion
}
