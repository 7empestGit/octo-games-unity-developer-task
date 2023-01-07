using App.GameEvents;
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

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<PlayerHealthChanged> (PlayerGotDamagedEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<PlayerHealthChanged> (PlayerGotDamagedEventHandler);
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

    #endregion
  }
}