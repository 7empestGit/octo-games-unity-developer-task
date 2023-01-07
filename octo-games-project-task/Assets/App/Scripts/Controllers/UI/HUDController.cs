using App.GameEvents;
using DynamicBox.EventManagement;
using UnityEngine;
using UnityEngine.UI;

namespace App.Controllers.UI
{
  public class HUDController : MonoBehaviour
  {
    [SerializeField] private Slider healthSlider;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<PlayerGotDamagedEvent> (PlayerGotDamagedEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<PlayerGotDamagedEvent> (PlayerGotDamagedEventHandler);
    }

    #endregion


    #region Event Handlers

    private void PlayerGotDamagedEventHandler (PlayerGotDamagedEvent eventDetails)
    {
      healthSlider.value = eventDetails.CurrentPlayerHealth;
    }

    #endregion
  }
}