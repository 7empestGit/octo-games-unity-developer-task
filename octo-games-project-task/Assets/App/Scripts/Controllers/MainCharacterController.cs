using UnityEngine;
using DynamicBox.EventManagement;
using App.GameEvents;
using Opsive.Shared.Input;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Traits;

namespace App.Controllers
{
  public class MainCharacterController : MonoBehaviour
  {
    private CharacterHealth characterHealth;
    private CharacterRespawner characterRespawner;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
      EventManager.Instance.AddListener<RestartGameEvent> (RestartGameEventHandler);
    }

    void Awake ()
    {
      ToggleInput (false);
      characterHealth = GetComponent<CharacterHealth> ();
      characterRespawner = GetComponent<CharacterRespawner> ();
    }

    void OnDisable ()
    {
      EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
      EventManager.Instance.AddListener<RestartGameEvent> (RestartGameEventHandler);
    }

    #endregion

    public void OnPlayerDamage ()
    {
      EventManager.Instance.Raise (new PlayerHealthChanged (characterHealth.HealthValue));
    }

    public void OnPlayerDeath ()
    {
      ToggleInput (false);
      EventManager.Instance.Raise (new PlayerIsDeadEvent ());
    }

    public void OnPlayerRespawn ()
    {
      ToggleInput (true);
      EventManager.Instance.Raise (new PlayerHealthChanged (characterHealth.HealthValue));
    }

    #region Event Handlers

    private void StartGameEventHandler (StartGameEvent eventDetails)
    {
      ToggleInput (true);
    }

    private void RestartGameEventHandler (RestartGameEvent eventDetails)
    {
      characterRespawner.Respawn ();
    }

    #endregion

    private void ToggleInput (bool value)
    {
      Cursor.visible = !value;
      Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
      GetComponent<UnityInput> ().enabled = value;
      GetComponent<UltimateCharacterLocomotionHandler> ().enabled = value;
    }
  }
}