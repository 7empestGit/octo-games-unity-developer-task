using App.Enums;
using App.GameEvents;
using App.GameEvents.UI;
using DynamicBox.EventManagement;
using UnityEngine;

namespace App.Managers.UI
{
  public class UIManager : MonoBehaviour
  {
    #region Unity Methods

    void OnEnable ()
    {
      EventManager.Instance.AddListener<PlayerIsDeadEvent> (PlayerIsDeadEventHandler);
    }

    void OnDisable ()
    {
      EventManager.Instance.RemoveListener<PlayerIsDeadEvent> (PlayerIsDeadEventHandler);
    }

    #endregion

    #region Event Handlers

    private void PlayerIsDeadEventHandler (PlayerIsDeadEvent eventDetails)
    {
      EventManager.Instance.Raise (new ShowPopupEvent (PopupType.DeathScreen));
    }

    #endregion
  }
}
