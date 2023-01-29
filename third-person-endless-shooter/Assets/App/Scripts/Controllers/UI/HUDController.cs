using App.GameEvents;
using App.GameEvents.UI;
using DynamicBox.EventManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Controllers.UI
{
  public class HUDController : MonoBehaviour
  {
    [Header ("Links")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    [Space]
    [SerializeField] private TMP_Text currentKilledEnemiesText;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<PlayerHealthChanged> (PlayerGotDamagedEventHandler);
      EventManager.Instance.AddListener<UpdateHUDEvent> (UpdateHUDEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<PlayerHealthChanged> (PlayerGotDamagedEventHandler);
      EventManager.Instance.RemoveListener<UpdateHUDEvent> (UpdateHUDEventHandler);
    }

    #endregion


    private void UpdateSliderValues (float currentHealth)
    {
      healthSlider.value = currentHealth;
      healthText.text = $"{currentHealth} / 100";
    }

    #region Event Handlers

    private void PlayerGotDamagedEventHandler (PlayerHealthChanged eventDetails)
    {
      UpdateSliderValues (eventDetails.CurrentPlayerHealth);
    }

    private void UpdateHUDEventHandler (UpdateHUDEvent eventDetails)
    {
      currentKilledEnemiesText.text = eventDetails.CurrentKilledEnemies.ToString ();
    }

    #endregion
  }
}