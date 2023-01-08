using App.Enums;
using App.GameEvents;
using App.GameEvents.UI;
using DynamicBox.EventManagement;
using System.Collections.Generic;
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
      int currentKilledEnemies = PlayerPrefs.GetInt ("CurrentKilledEnemies");
      int killedEnemiesRecord = PlayerPrefs.GetInt ("KilledEnemiesRecord");

      EventManager.Instance.Raise (new ShowPopupEvent (PopupType.DeathScreen));

      List<object> popupParameters = new List<object> () { currentKilledEnemies, killedEnemiesRecord };
      EventManager.Instance.Raise (new InitializePopupEvent (PopupType.DeathScreen, popupParameters));
    }

    #endregion
  }
}
