using UnityEngine;
using DynamicBox.EventManagement;
using App.GameEvents;
using Opsive.Shared.Input;
using Opsive.UltimateCharacterController.Character;

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
    character.GetComponent<UnityInput> ().enabled = true;
    character.GetComponent<UltimateCharacterLocomotionHandler> ().enabled = true;
  }

  #endregion
}
