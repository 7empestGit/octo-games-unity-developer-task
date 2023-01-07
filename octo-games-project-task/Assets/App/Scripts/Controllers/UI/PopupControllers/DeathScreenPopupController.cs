using App.Enums;
using App.GameEvents;
using App.GameEvents.UI;
using DynamicBox.EventManagement;
using TMPro;
using UnityEngine;

namespace App.Controllers.UI.Popups
{
  public class DeathScreenPopupController : MonoBehaviour
  {
    [Header ("Parameters")]
    [SerializeField] private PopupType popupType;

    [Header ("Links")]
    [SerializeField] private TMP_Text currentKillsText;
    [SerializeField] private TMP_Text recordKillsText;
    [SerializeField] private TMP_Text newRecordText;

    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<InitializePopupEvent> (InitializePopupEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<InitializePopupEvent> (InitializePopupEventHandler);
    }

    #endregion

    public void Initialize (int currentKills, int recordKills)
    {
      currentKillsText.text = $"Current Kills: {currentKills}";
      recordKillsText.text = $"Record Kills: {recordKills}";

      bool isNewRecordSet = currentKills >= recordKills;
      newRecordText.enabled = isNewRecordSet;
    }

    public void OnRespawnButtonClicked ()
    {
      EventManager.Instance.Raise (new RestartGameEvent ());
      ClosePopup ();
    }

    private void ClosePopup ()
    {
      Destroy (gameObject);
    }

    #region Event Handlers

    private void InitializePopupEventHandler (InitializePopupEvent eventDetails)
    {
      if (eventDetails.PopupType != popupType)
        return;

      int _currentKills = (int)eventDetails.PopupParameters[0];
      int _recordKills = (int)eventDetails.PopupParameters[1];

      Initialize (_currentKills, _recordKills);
    }

    #endregion
  }
}
