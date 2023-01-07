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

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
      ToggleInput (false);
    }

    void Awake ()
    {
      characterHealth = GetComponent<CharacterHealth> ();
    }

    void OnDisable ()
    {
      EventManager.Instance.AddListener<StartGameEvent> (StartGameEventHandler);
    }

    #endregion

    public void OnPlayerDamage ()
    {
      EventManager.Instance.Raise (new PlayerHealthChanged (characterHealth.HealthValue));
    }


    public void OnPlayerDeath ()
    {
      EventManager.Instance.Raise (new PlayerIsDeadEvent ());
    }

    public void OnPlayerRespawn ()
    {
      EventManager.Instance.Raise (new PlayerHealthChanged (characterHealth.HealthValue));
    }

    #region Event Handlers

    private void StartGameEventHandler (StartGameEvent eventDetails)
    {
      ToggleInput (true);
    }

    #endregion

    private void ToggleInput (bool value)
    {
      GetComponent<UnityInput> ().enabled = value;
      GetComponent<UltimateCharacterLocomotionHandler> ().enabled = value;
    }
  }
}